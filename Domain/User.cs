using System;
using Microsoft.AspNetCore.Identity;

namespace Domain;

public class User : IdentityUser
{
    public string? DisplayName { get; set; }

    //Relationships
    public ICollection<MedicalAppointment> MedicalAppointments { get; set; } = [];
}
