using System;
using Domain;
using MediatR;
using Persistence;

namespace Application.Pets.Commands;

public class CreatePet
{
     public class Command : IRequest<string>
    {
         public required Pet Pet {get; set;}
    }

    public class Handler(AppDbContext context) : IRequestHandler<Command, string>
    {
        public async Task<string> Handle(Command request, CancellationToken cancellationToken)
        {
           context.Pets.Add(request.Pet);
           await context.SaveChangesAsync(cancellationToken);
           return request.Pet.Id;
        }
    }
}
