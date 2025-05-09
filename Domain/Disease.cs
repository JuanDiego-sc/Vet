using System;
using System.ComponentModel.DataAnnotations;
using Domain.Validations;

namespace Domain;

public class Disease
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [SpecialCharactersValidation]
    public required string Name { get; set; }
    [SpecialCharactersValidation]
    public required string Type { get; set; }
    [SpecialCharactersValidation]
    public required string Description { get; set; }
    public required DateTime CreateAt { get; set; }
    public required DateTime UpdateAt { get; set; }

    
    #region Relationships 
    public ICollection<AppointmentDetail>? AppointmentDetails { get; set; } = [];

    #endregion
}

