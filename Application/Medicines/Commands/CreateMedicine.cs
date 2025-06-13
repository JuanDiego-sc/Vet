using System;
using Application.DTOs;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Medicines.Commands;

public class CreateMedicine
{
    public class Command : IRequest<string>
    {
         public required MedicineDto MedicineDto {get; set;}
    }

    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Command, string>
    {
        public async Task<string> Handle(Command request, CancellationToken cancellationToken)
        {
            var medicine = mapper.Map<Medicine>(request.MedicineDto);
            context.Medicines.Add(medicine);
            await context.SaveChangesAsync(cancellationToken);
            return medicine.Id;
        }
    }
}
