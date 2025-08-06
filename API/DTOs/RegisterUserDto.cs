using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class RegisterUserDto
{
    [Required]
    public string Name { get; set; } = "";
    [Required]
    [EmailAddress]
    public string Email { get; set; } = "";
    //Password is validated automatically by identity 
    public string Password { get; set; } = "";
}
