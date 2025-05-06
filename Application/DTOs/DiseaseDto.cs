using System;

namespace Application.DTOs;

public class DiseaseDto
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string Type { get; set; }
    public required string Description { get; set; }

    public ICollection<AppointmentDetailDto> AppointmentDetails { get; set; } = [];

}

