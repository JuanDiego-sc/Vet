using System;

namespace Application.DTOs;

public class TreatmentDto
{
    public string? Id { get; set; } = Guid.NewGuid().ToString();
    public int? Duration {get; set;}
    public string? Dose {get; set;}
    public string? Contraindications {get; set;}
    public string? IdMedicine { get; set; } 
    public string? MedicineName {get; set;}
    public  string? IdDetail { get; set; } 
    public AppointmentDetailDto? AppointmentDetail {get; set;} = null!;

}
