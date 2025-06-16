using System;
using Application.DTOs;
using FluentValidation;

namespace Application.Diseases.Validators;

public class BaseDiseaseValidator<T, TDto> : AbstractValidator<T>
    where TDto : DiseaseDto
{
    public BaseDiseaseValidator(Func<T, TDto> selector)
    {
        RuleFor(x => selector(x).Name)
        .NotEmpty()
        .WithMessage("Disease Name is required");

        RuleFor(x => selector(x).Description)
        .NotEmpty()
        .WithMessage("Disease Description is required");
    }
}
