using System;
using Domain;
using MediatR;
using Persistence;

namespace Application.Treatments.Commands;

public class CreateTreatment
{
     public class Command : IRequest<string>
    {
         public required Treatment Treatment {get; set;}
    }

    public class Handler(AppDbContext context) : IRequestHandler<Command, string>
    {
        public async Task<string> Handle(Command request, CancellationToken cancellationToken)
        {
           context.Treatments.Add(request.Treatment);
           await context.SaveChangesAsync(cancellationToken);
           return request.Treatment.Id;
        }
    }
}
