using System;
using Domain;

namespace Application.DTOs;

public class MedicalAppointmentDto
{
    public string? Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime AppointmentDate {get; set;}
    public Status? AppointmentStatus {get; set;}
    public string? Reason {get; set;}

    public string? IdPet { get; set; } 
    public string? PetName {get; set;}
    public ICollection<AppointmentDetailDto>? AppointmentDetails { get; set; } = [];

}
