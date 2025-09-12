using System;

namespace Domain;

public class Message
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string BodyMessage { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    //navigation properties 
    public required string BlogId { get; set; }
    public Blog Blog { get; set; } = null!;
    public required string UserId { get; set; }
    public User User { get; set; } = null!;
}
