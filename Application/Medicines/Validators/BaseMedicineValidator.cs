using System;
using System.Security.Cryptography.X509Certificates;
using Application.DTOs;
using FluentValidation;

namespace Application.Medicines.Validators;

public class BaseMedicineValidator<T, TDto> : AbstractValidator<T>
    where TDto : MedicineDto
{
    public BaseMedicineValidator(Func<T, TDto> selector)
    {
        RuleFor(x => selector(x).Name)
        .NotEmpty()
        .WithMessage("Medicine Name is required");

        RuleFor(x => selector(x).Stock)
        .NotEmpty()
        .WithMessage("Medicine stock is required");

        RuleFor(x => selector(x).Description)
        .NotEmpty()
        .WithMessage("Medicine Description is required");
    }
}
