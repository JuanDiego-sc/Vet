using System;
using Domain;

namespace Application.DTOs;

public class MedicalAppointmentDto
{
    public required string Id { get; set; } 
    public DateTime AppointmentDate {get; set;}
    public required Status AppointmentStatus {get; set;}
    public required string Reason {get; set;}

    public required string IdPet { get; set; } 
    public PetDto Pet {get; set;} = null!;
    public ICollection<AppointmentDetailDto> AppointmentDetails { get; set; } = [];

}
