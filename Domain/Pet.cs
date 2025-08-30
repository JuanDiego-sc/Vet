using System;
using System.ComponentModel.DataAnnotations;
using Domain.Validations;

namespace Domain;

public class Pet
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [SpecialCharactersValidation]
    public required string PetName { get; set; }

    [SpecialCharactersValidation]
    public required string Breed { get; set; }

    [SpecialCharactersValidation]
    public required string Species { get; set; }

    [SpecialCharactersValidation]
    public required string Gender { get; set; }

    public DateTime Birthdate { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdateAt { get; set; } = DateTime.UtcNow;

    #region Relationships
    public ICollection<MedicalAppointment> MedicalAppointments { get; set; } = [];
    
    #endregion

}
