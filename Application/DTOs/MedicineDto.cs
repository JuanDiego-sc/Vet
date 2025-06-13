using System;

namespace Application.DTOs;

public class MedicineDto
{
    public string? Id { get; set; } = Guid.NewGuid().ToString();
    public string? Name { get; set; }
    public int Stock { get; set; }
    public string? Description { get; set; }

}
