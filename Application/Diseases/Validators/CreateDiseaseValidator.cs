using System;
using Application.Diseases.Commands;
using Application.DTOs;

namespace Application.Diseases.Validators;

public class CreateDiseaseValidator : BaseDiseaseValidator<CreateDisease.Command, DiseaseDto>
{
    public CreateDiseaseValidator() : base(x => x.DiseaseDto){}
}
