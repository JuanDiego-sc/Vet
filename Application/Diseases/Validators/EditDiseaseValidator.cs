using System;
using System.Data;
using Application.Diseases.Commands;
using Application.DTOs;
using FluentValidation;

namespace Application.Diseases.Validators;

public class EditDiseaseValidator : BaseDiseaseValidator<EditDisease.Command, DiseaseDto>
{
    public EditDiseaseValidator() : base(x => x.DiseaseDto)
    {
        RuleFor(x => x.DiseaseDto.Id)
        .NotEmpty()
        .WithMessage("Disease Id is required");
    }
}
