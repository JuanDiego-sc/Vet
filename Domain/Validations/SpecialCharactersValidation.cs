using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Domain.Validations;

public class SpecialCharactersValidation : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
            return ValidationResult.Success;

        string? stringValue = value.ToString();
        if (string.IsNullOrEmpty(stringValue))
            return ValidationResult.Success;

        // Patrón que permite letras, números, espacios y algunos caracteres básicos
        string pattern = @"^[a-zA-Z0-9\s\-_.,ñÑáéíóúÁÉÍÓÚ]+$";
        
        if (!Regex.IsMatch(stringValue, pattern))
        {
            return new ValidationResult(
                "El campo "+ stringValue + " no puede contener caracteres especiales o símbolos no permitidos.");
        }

        return ValidationResult.Success;
    }
} 