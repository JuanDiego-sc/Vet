using System;
using System.ComponentModel.DataAnnotations;
using Domain.Validations;

namespace Domain;

public class Treatment
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required int Duration {get; set;}
    [SpecialCharactersValidation]
    public required string Dose {get; set;}
    [SpecialCharactersValidation]
    public required string Contraindications {get; set;}
    public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    public  DateTime UpdateAt { get; set; } = DateTime.UtcNow;

    #region Relationships 

    public string? IdMedicine {get; set;}
    public Medicine Medicine {get; set;} = null!;
    public string? IdDetail {get; set;}
    public AppointmentDetail AppointmentDetail {get; set;} = null!;

    #endregion
}
