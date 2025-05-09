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
    public required DateTime CreateAt { get; set; }
    public required DateTime UpdateAt { get; set; }

    #region Relationships 

    public string? IdMedicine {get; set;}
    public Medicine? Medicine {get; set;} = null!;
    public string? IdDetail {get; set;}
    public AppointmentDetail? AppointmentDetail {get; set;} = null!;

    #endregion
}
