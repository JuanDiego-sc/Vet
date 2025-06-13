using System;

namespace Application.DTOs;

public class DiseaseDto
{
    public string? Id { get; set; } = Guid.NewGuid().ToString();
    public string? Name { get; set; }
    public string? Type { get; set; }
    public string? Description { get; set; }

}

