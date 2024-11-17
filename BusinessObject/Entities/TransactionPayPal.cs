using System;
using System.Collections.Generic;

namespace PayPalRepositories.Entities;

public partial class TransactionPayPal
{
    public string PaymentId { get; set; } = null!;

    public string PayerId { get; set; } = null!;

    public string Amount { get; set; } = null!;
}
