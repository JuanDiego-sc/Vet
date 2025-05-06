using System;

namespace Application.DTOs;

public class TreatmentDto
{
    public required string Id { get; set; }
    public required int Duration {get; set;}
    public required string Dose {get; set;}
    public required string Contraindications {get; set;}
    public required string IdMedicine { get; set; } 
    public MedicineDto Medicine {get; set;} = null!;
    public required string IdAppointmentDetail { get; set; } 
    public AppointmentDetailDto AppointmentDetail {get; set;} = null!;

}
