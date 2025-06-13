using System;
using Application.DTOs;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Medicines.Commands;

public class EditMedicine
{
    public class Command : IRequest{
        public required MedicineDto MedicineDto {get; set;}

        public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var detail = await context.Medicines
                .FindAsync([request.MedicineDto.Id], cancellationToken) 
             ?? 
            throw new Exception("Appointment not found");

            mapper.Map(request.MedicineDto, detail); 
            await context.SaveChangesAsync(cancellationToken);
        }
    }
    }
}
