using System;
using Application.DTOs;
using FluentValidation;

namespace Application.Pets.Validators;

public class CreatePetValidator<T, TDto> : AbstractValidator<T>
where TDto : PetDto
{
    public CreatePetValidator(Func<T, TDto> selector)
    {
        RuleFor(x => selector(x).PetName)
        .NotEmpty()
        .WithMessage("Pet Name is required");

        RuleFor(x => selector(x).Breed)
        .NotEmpty()
        .WithMessage("Breed is required");

         RuleFor(x => selector(x).Gender)
        .NotEmpty()
        .WithMessage("Gender is required");
    }
}
