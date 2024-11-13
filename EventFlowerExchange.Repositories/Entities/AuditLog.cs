using System;
using System.Collections.Generic;

namespace EventFlowerExchange.Repositories.Entities;

public partial class AuditLog
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string? Action { get; set; }

    public string? Description { get; set; }

    public DateTime? ActionDate { get; set; }

    public virtual User? User { get; set; }
}
