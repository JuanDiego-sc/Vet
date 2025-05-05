using System;
using System.ComponentModel.DataAnnotations;

namespace Domain;

public class MedicalAppointment
{
    [Key]
    public string Id { get; set; } = new Guid().ToString();
    public DateTime AppointmentDate {get; set;}
    public required Status AppointmentStatus {get; set;}
    public required string Reason {get; set;}
    public required DateTime CreateAt { get; set; }
    public required DateTime UpdateAt { get; set; }

    #region Relationships 

    public string? IdPet {get; set;}
    public Pet Pet {get; set;} = null!;
    public ICollection<AppointmentDetail> AppointmentDetails { get; set; } = [];

    #endregion
}
