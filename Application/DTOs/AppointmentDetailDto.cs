using System;

namespace Application.DTOs;

public class AppointmentDetailDto
{
    public string? Id { get; set; }
    public string? Diagnostic {get; set;}
    public string? Observation {get; set;}
    public string? IdDisease { get; set; }  
    public string? DiseaseName {get; set;}
    public string? IdAppointment { get; set; }
    public ICollection<TreatmentDto>? Treatments { get; set; } = [];

}
   