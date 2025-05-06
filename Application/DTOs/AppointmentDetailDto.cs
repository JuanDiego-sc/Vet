using System;

namespace Application.DTOs;

public class AppointmentDetailDto
{
    public string? Id { get; set; } = new Guid().ToString();
    public string? Diagnostic {get; set;}
    public string? Observation {get; set;}
    public string? IdDisease { get; set; }  
    public DiseaseDto? Disease {get; set;} = null!;
    public string? IdMedicalAppointment { get; set; }
    public MedicalAppointmentDto? MedicalAppointment {get; set;} = null!;
    public ICollection<TreatmentDto> Treatments { get; set; } = [];

}
   