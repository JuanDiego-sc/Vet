using System;
using MediatR;
using Persistence;

namespace Application.Medicines.Commands;

public class DeleteMedicine
{
    public class Command : IRequest{
        public required string Id {get; set;}
    }

    public class Handler(AppDbContext context) : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var medicine = await context.Medicines
                .FindAsync([request.Id], cancellationToken) 
             ?? 
            throw new Exception("Medicine not found");

            context.Remove(medicine); 
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
