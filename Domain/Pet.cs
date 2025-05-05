using System;
using System.ComponentModel.DataAnnotations;


namespace Domain;

public class Pet
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string PetName { get; set; }
    public required string Breed { get; set; }
    public required string Species { get; set; }
    public required string Gender { get; set; }
    public DateTime Birthdate { get; set; }
    public required DateTime CreateAt { get; set; }
    public required DateTime UpdateAt { get; set; }

    #region Relationships
    public ICollection<MedicalAppointment> MedicalAppointments { get; set; } = [];
    
    #endregion

}
