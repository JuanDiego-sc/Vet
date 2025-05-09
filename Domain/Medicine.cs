using System;
using System.ComponentModel.DataAnnotations;
using Domain.Validations;

namespace Domain;

public class Medicine
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    [SpecialCharactersValidation]
    public required string Name { get; set; }
    public required int Stock { get; set; }
    [SpecialCharactersValidation]
    public required string Description { get; set; }
    public  DateTime CreateAt { get; set; } = DateTime.UtcNow;
    public  DateTime UpdateAt { get; set; } = DateTime.UtcNow;

    #region Relationships 
    public ICollection<Treatment>? Treatments { get; set; } = [];

    #endregion
}
