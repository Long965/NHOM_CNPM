using System;
using System.Collections.Generic;

namespace EventFlowerExchange.Repositories.Entities;

public partial class OrderDetail
{
    public int Id { get; set; }

    public int? OrderId { get; set; }

    public int? FlowerId { get; set; }

    public int? Quantity { get; set; }

    public decimal? Price { get; set; }

    public decimal? Subtotal { get; set; }

    public virtual Flower? Flower { get; set; }

    public virtual Order? Order { get; set; }
}
