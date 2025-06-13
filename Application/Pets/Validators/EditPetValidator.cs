using System;
using Application.DTOs;
using Application.Pets.Commands;
using FluentValidation;

namespace Application.Pets.Validators;

public class EditPetValidator<T, TDto> : AbstractValidator<T>
where TDto : PetDto
{
    //TODO: Refactor validations to avoid code smell
    public EditPetValidator(Func<T, TDto> selector)
    {
        RuleFor(x => selector(x).Id)
        .NotEmpty()
        .WithMessage("Pet Id is required");

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

