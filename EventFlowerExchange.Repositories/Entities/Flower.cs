using System;
using System.Collections.Generic;

namespace EventFlowerExchange.Repositories.Entities;

public partial class Flower
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? Quantity { get; set; }

    public string? Condition { get; set; }

    public decimal? PricePerUnit { get; set; }

    public int? SellerId { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    public virtual ICollection<FlowerImage> FlowerImages { get; set; } = new List<FlowerImage>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual User? Seller { get; set; }
}
