using System;
using System.ComponentModel.DataAnnotations;

namespace Domain;

public class AppointmentDetail
{
    [Key]
    public string Id { get; set; } = new Guid().ToString();
    public required string Diagnostic {get; set;}
    public required string Observation {get; set;}
    public required DateTime CreateAt { get; set; }
    public required DateTime UpdateAt { get; set; }

    #region Relationships 

    public string? IdDisease {get; set;}
    public Disease Disease {get; set;} = null!;
    public string? IdAppointment {get; set;}
    public MedicalAppointment MedicalAppointment {get; set;} = null!;
    public ICollection<Treatment> Treatments { get; set; } = [];

    #endregion
}
