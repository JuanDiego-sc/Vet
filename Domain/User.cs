using System;

namespace Domain;

public class User
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string Email { get; set; }
    public required string Name { get; set; }
    public required string Password { get; set; }
    public required string UserName { get; set; }
    public DateTime? CreateAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdateAt { get; set; } = DateTime.UtcNow;
}
