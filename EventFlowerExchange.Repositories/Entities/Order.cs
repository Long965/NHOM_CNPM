using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EventFlowerExchange.Repositories.Entities;

public partial class Order
{
    public int Id { get; set; }

    public int? BuyerId { get; set; }

    public int? SellerId { get; set; }

    public int? FlowerId { get; set; }

    public int? Quantity { get; set; }

    public DateOnly? OrderDate { get; set; }

    public string? Status { get; set; }

    public decimal? TotalPrice { get; set; }

    [JsonIgnore]
    public virtual User? Buyer { get; set; }

    [JsonIgnore]
    public virtual Flower? Flower { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    [JsonIgnore]
    public virtual User? Seller { get; set; }
}
