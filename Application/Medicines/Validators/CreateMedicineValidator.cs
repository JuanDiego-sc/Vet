using System;
using Application.DTOs;
using Application.Medicines.Commands;

namespace Application.Medicines.Validators;

public class CreateMedicineValidator : BaseMedicineValidator<CreateMedicine.Command, MedicineDto>
{
    public CreateMedicineValidator() : base(x => x.MedicineDto){}
}
