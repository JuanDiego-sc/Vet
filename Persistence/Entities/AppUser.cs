using System;
using Microsoft.AspNetCore.Identity;

namespace Persistence.Entities;

public class AppUser : IdentityUser
{
    //*Temporally props until define business rules
    public string? DisplayName { get; set; }
    public string? Bio { get; set; }
    public string? ImageUrl { get; set; }

}
