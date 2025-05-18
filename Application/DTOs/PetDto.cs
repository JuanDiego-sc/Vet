using System;

namespace Application.DTOs;

public class PetDto
{
    public string? Id { get; set; }
    public string? PetName { get; set; }
    public string? Breed { get; set; }
    public string? Species { get; set; }
    public string? Gender { get; set; }
    public DateTime? Birthdate { get; set; }

    public ICollection<MedicalAppointmentDto>? MedicalAppointments { get; set; } = [];
    
}
