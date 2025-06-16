using System;
using Application.DTOs;
using Application.Pets.Commands;
using FluentValidation;

namespace Application.Pets.Validators;

public class CreatePetValidator : BasePetValidator<CreatePet.Command, PetDto>
{
    public CreatePetValidator() : base(x => x.PetDto)
    {
    }
}
