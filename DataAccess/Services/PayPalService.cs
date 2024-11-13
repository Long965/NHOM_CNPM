using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PaypalDemo.Services
{
    public class PayPalService
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;

        public PayPalService(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
        }

        private async Task<string> GetAccessTokenAsync()
        {
            var clientId = _configuration["PayPal:ClientId"];
            var clientSecret = _configuration["PayPal:ClientSecret"];
            var authToken = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));

            var request = new HttpRequestMessage(HttpMethod.Post, $"{_configuration["PayPal:BaseUrl"]}/v1/oauth2/token");
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", authToken);
            request.Content = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded");

            var response = await _client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                var errorDetails = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Request failed with status code {response.StatusCode}: {errorDetails}");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var accessTokenResponse = JsonConvert.DeserializeObject<AccessTokenResponse>(jsonResponse);

            return accessTokenResponse.AccessToken;
        }

        public async Task<string> CreatePaymentAsync(decimal amount, string returnUrl, string cancelUrl)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be greater than zero.");
            }

            var accessToken = await GetAccessTokenAsync();

            var paymentRequest = new
            {
                intent = "sale",
                payer = new { payment_method = "paypal" },
                transactions = new[]
                {
                    new
                    {
                        amount = new { total = amount.ToString("F2"), currency = "USD" },
                        description = "Payment description"
                    }
                },
                redirect_urls = new { return_url = returnUrl, cancel_url = cancelUrl }
            };

            var requestContent = new StringContent(JsonConvert.SerializeObject(paymentRequest), Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, $"{_configuration["PayPal:BaseUrl"]}/v1/payments/payment");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            request.Content = requestContent;

            var response = await _client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                var errorDetails = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Payment creation failed with status code {response.StatusCode}: {errorDetails}");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            return jsonResponse; // This contains the approval link
        }

        public async Task<PayPalPaymentResponse?> ExecutePaymentAsync(string paymentId, string payerId)
        {
            if (string.IsNullOrWhiteSpace(paymentId) || string.IsNullOrWhiteSpace(payerId))
            {
                throw new ArgumentException("PaymentId and PayerId cannot be null or empty.");
            }

            var accessToken = await GetAccessTokenAsync();

            var request = new HttpRequestMessage(HttpMethod.Post, $"{_configuration["PayPal:BaseUrl"]}/v1/payments/payment/{paymentId}/execute");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            request.Content = new StringContent(JsonConvert.SerializeObject(new { payer_id = payerId }), Encoding.UTF8, "application/json");

            var response = await _client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                var errorDetails = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Payment execution failed with status code {response.StatusCode}: {errorDetails}");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var paymentResponse = JsonConvert.DeserializeObject<PayPalPaymentResponse>(jsonResponse);
            return paymentResponse;
        }
    }

    public class AccessTokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }
}