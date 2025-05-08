using System;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Pets.Commands;

public class EditPet
{
    public class Command : IRequest{
        public required Pet Pet {get; set;}

        public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var pet = await context.Pets
                .FindAsync([request.Pet.Id], cancellationToken) 
             ?? 
            throw new Exception("Pet not found");

            mapper.Map(request.Pet, pet); 
            await context.SaveChangesAsync(cancellationToken);
        }
    }
    }
}
