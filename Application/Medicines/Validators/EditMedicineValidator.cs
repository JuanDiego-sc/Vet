using System;
using Application.DTOs;
using Application.Medicines.Commands;
using FluentValidation;

namespace Application.Medicines.Validators;

public class EditMedicineValidator : BaseMedicineValidator<EditMedicine.Command, MedicineDto>
{
    public EditMedicineValidator() : base(x => x.MedicineDto)
    {
        RuleFor(x => x.MedicineDto.Id)
        .NotEmpty()
        .WithMessage("Medicine Id is required");
    }
}
