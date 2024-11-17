using System.ComponentModel.DataAnnotations;

namespace PaypalDemo.Models
{
    public class TransactionPayPal
    {
        [Key]
        public string PaymentId { get; set; }
        public string PayerId { get; set; }
        public string Amount { get; set; }
    }
}
