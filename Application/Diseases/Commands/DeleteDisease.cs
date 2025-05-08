using System;
using MediatR;
using Persistence;

namespace Application.Diseases.Commands;

public class DeleteDisease
{
    public class Command : IRequest{
        public required string Id {get; set;}
    }

    public class Handler(AppDbContext context) : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var detail = await context.Diseases
                .FindAsync([request.Id], cancellationToken) 
             ?? 
            throw new Exception("Disease not found");

            context.Remove(detail); 
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
