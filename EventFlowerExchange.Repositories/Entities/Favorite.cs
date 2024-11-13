using System;
using System.Collections.Generic;

namespace EventFlowerExchange.Repositories.Entities;

public partial class Favorite
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? FlowerId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Flower? Flower { get; set; }

    public virtual User? User { get; set; }
}
