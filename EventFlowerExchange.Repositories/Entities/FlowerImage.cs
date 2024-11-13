using System;
using System.Collections.Generic;

namespace EventFlowerExchange.Repositories.Entities;

public partial class FlowerImage
{
    public int Id { get; set; }

    public int? FlowerId { get; set; }

    public string? ImageUrl { get; set; }

    public string? AltText { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Flower? Flower { get; set; }
}
