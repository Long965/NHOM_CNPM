using System;
using System.Collections.Generic;

namespace EventFlowerExchange.Repositories.Entities;

public partial class UserRole
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string? RoleName { get; set; }

    public DateTime? AssignedAt { get; set; }

    public virtual User? User { get; set; }
}
