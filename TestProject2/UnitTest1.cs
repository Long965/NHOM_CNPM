using Microsoft.Extensions.Configuration;  
using NUnit.Framework;
using PaypalDemo.Services;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using System.Collections.Generic;

namespace PaypalDemo.Tests
{
    public class CustomHttpMessageHandler : HttpMessageHandler
    {
        private readonly HttpResponseMessage _response;

        public CustomHttpMessageHandler(HttpResponseMessage response)
        {
            _response = response;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            return Task.FromResult(_response);
        }
    }

    [TestFixture]
    public class PayPalServiceTests
    {
        private IConfiguration _configuration;
        private HttpClient _httpClient;
        private PayPalService _payPalService;

        [SetUp]
        public void SetUp()
        {
            // Tạo đối tượng IConfiguration với các giá trị cài sẵn
            var inMemorySettings = new Dictionary<string, string>
            {
                {"PayPal:ClientId", "test-client-id"},
                {"PayPal:ClientSecret", "test-client-secret"},
                {"PayPal:BaseUrl", "https://api.sandbox.paypal.com"}
            };

            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)  // Thêm cấu hình vào bộ nhớ
                .Build();

            // Tạo HttpClient với CustomHttpMessageHandler
            var mockResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{\"id\":\"mock-payment-id\"}")  // Giả lập phản hồi từ PayPal API
            };

            var handler = new CustomHttpMessageHandler(mockResponse);
            _httpClient = new HttpClient(handler);

            // Khởi tạo PayPalService với HttpClient và IConfiguration
            _payPalService = new PayPalService(_httpClient, _configuration);
        }

        [Test]
        public async Task Test_CreatePaymentAsync_ReturnsJsonResponse()
        {
            // Gọi phương thức CreatePaymentAsync
            var result = await _payPalService.CreatePaymentAsync(100.0m, "https://example.com/return", "https://example.com/cancel");

            // Kiểm tra xem kết quả trả về có chứa ID mock
            Assert.IsTrue(result.Contains("mock-payment-id"), "Payment ID không khớp với ID giả lập.");
        }
    }
}
