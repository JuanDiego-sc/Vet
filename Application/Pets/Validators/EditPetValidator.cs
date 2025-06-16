using System;
using Application.DTOs;
using Application.Pets.Commands;
using FluentValidation;

namespace Application.Pets.Validators;

public class EditPetValidator : BasePetValidator<EditPet.Command, PetDto>
{
    public EditPetValidator() : base(x => x.PetDto)
    {
        RuleFor(x => x.PetDto.Id)
        .NotEmpty()
        .WithMessage("Pet Id is required");
    }
}

