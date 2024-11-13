using System;
using System.Collections.Generic;

namespace EventFlowerExchange.Repositories.Entities;

public partial class Discount
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public decimal? DiscountPercentage { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public bool? IsActive { get; set; }
}
