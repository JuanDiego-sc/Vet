using System;

namespace Domain;

public class Blog
{
    public required string Id { get; set; } = Guid.NewGuid().ToString();
    public required string Title { get; set; }
    public required string Description { get; set; }

    //navigation properties
    public ICollection<Message> Messages { get; set; } = [];
}
