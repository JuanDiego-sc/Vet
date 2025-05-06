using System;

namespace Application.DTOs;

public class MedicineDto
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required int Stock { get; set; }
    public required string Description { get; set; }

    public ICollection<TreatmentDto> Treatments { get; set; } = [];

}
