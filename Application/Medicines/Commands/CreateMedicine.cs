using System;
using Application.Core;
using Application.DTOs;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Medicines.Commands;

public class CreateMedicine
{
    public class Command : IRequest<Result<string>>
    {
         public required MedicineDto MedicineDto {get; set;}
    }

    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Command, Result<string>>
    {
        public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
        {
            var medicine = mapper.Map<Medicine>(request.MedicineDto);

            context.Medicines.Add(medicine);
            var result = await context.SaveChangesAsync(cancellationToken) > 0;

            if (!result) return Result<string>.Failure("Failure to create medicine", 400);
            return Result<string>.Success(medicine.Id);
        }
    }
}
