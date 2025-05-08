using System;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Medicines.Commands;

public class EditMedicine
{
    public class Command : IRequest{
        public required Medicine Medicine {get; set;}

        public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var detail = await context.Diseases
                .FindAsync([request.Medicine.Id], cancellationToken) 
             ?? 
            throw new Exception("Appointment not found");

            mapper.Map(request.Medicine, detail); 
            await context.SaveChangesAsync(cancellationToken);
        }
    }
    }
}
