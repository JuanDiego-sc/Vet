using System;

namespace Application.DTOs;

public class PetDto
{
    public required string Id { get; set; }
    public required string PetName { get; set; }
    public required string Breed { get; set; }
    public required string Species { get; set; }
    public required string Gender { get; set; }
    public DateTime Birthdate { get; set; }

    public ICollection<MedicalAppointmentDto> MedicalAppointments { get; set; } = [];
    
}
