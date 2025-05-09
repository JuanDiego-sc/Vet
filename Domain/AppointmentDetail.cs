using System;
using System.ComponentModel.DataAnnotations;
using Domain.Validations;

namespace Domain;

public class AppointmentDetail
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    [SpecialCharactersValidation]
    public required string Diagnostic {get; set;}
    [SpecialCharactersValidation]
    public required string Observation {get; set;}
    public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    public  DateTime UpdateAt { get; set; } = DateTime.UtcNow;

    #region Relationships 

    public string? IdDisease {get; set;}
    public Disease? Disease {get; set;} = null!;
    public string? IdAppointment {get; set;}
    public MedicalAppointment? MedicalAppointment {get; set;} = null!;
    public ICollection<Treatment>? Treatments { get; set; } = [];

    #endregion
}
