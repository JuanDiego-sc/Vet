using System;
using MediatR;
using Persistence;

namespace Application.Treatments.Commands;

public class DeleteTreatment
{
    public class Command : IRequest{
        public required string Id {get; set;}
    }

    public class Handler(AppDbContext context) : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var treatment = await context.Treatments
                .FindAsync([request.Id], cancellationToken) 
             ?? 
            throw new Exception("Treatment not found");

            context.Remove(treatment); 
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
