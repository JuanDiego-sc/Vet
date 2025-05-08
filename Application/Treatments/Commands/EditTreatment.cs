using System;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Treatments.Commands;

public class EditTreatment
{
    public class Command : IRequest{
        public required Treatment Treatment {get; set;}

        public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var treatment = await context.Treatments
                .FindAsync([request.Treatment.Id], cancellationToken) 
             ?? 
            throw new Exception("Treatment not found");

            mapper.Map(request.Treatment, treatment); 
            await context.SaveChangesAsync(cancellationToken);
        }
    }
    }
}
