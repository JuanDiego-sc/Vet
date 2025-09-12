using System;

namespace Application.DTOs;

public class MessageDto
{
    public required string Id { get; set; }
    public required string BodyMessage { get; set; }
    public DateTime CreatedAt { get; set; }
    public required string UserId { get; set; }
    public required string UserName { get; set; }
    public string? UserPhoto { get; set; }
}
