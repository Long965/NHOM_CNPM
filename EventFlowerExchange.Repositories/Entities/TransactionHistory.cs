using System;
using System.Collections.Generic;

namespace EventFlowerExchange.Repositories.Entities;

public partial class TransactionHistory
{
    public int Id { get; set; }

    public int? PaymentId { get; set; }

    public DateTime? TransactionDate { get; set; }

    public string? Status { get; set; }

    public string? Details { get; set; }

    public virtual Payment? Payment { get; set; }
}
