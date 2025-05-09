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
            if (request.Pet.Birthdate.Kind != DateTimeKind.Utc)
            {
                request.Pet.Birthdate = DateTime.SpecifyKind(request.Pet.Birthdate, DateTimeKind.Utc);
            } 
           context.Pets.Add(request.Pet);
           await context.SaveChangesAsync(cancellationToken);
           return request.Pet.Id;
        }
    }
}
