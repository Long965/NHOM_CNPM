using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EventFlowerExchange.Repositories.Entities;

public partial class UserProfile
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Gender { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? Address { get; set; }

    [JsonIgnore]
    public virtual User? User { get; set; }
}
