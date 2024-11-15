using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EventFlowerExchange.Repositories.Entities;

public partial class Token
{
    public int TokenId { get; set; }

    public int UserId { get; set; }

    public string Token1 { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime ExpiresAt { get; set; }

    public bool IsRevoked { get; set; }

    [JsonIgnore]
    public virtual User User { get; set; } = null!;
}
