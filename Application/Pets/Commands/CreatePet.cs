using System;
using Application.DTOs;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Pets.Commands;

public class CreatePet
{
    //TODO: Use DTOs for create, edit and delete request
     public class Command : IRequest<string>
    {
        public required PetDto PetDto { get; set; }
    }

    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Command, string>
    {
        public async Task<string> Handle(Command request, CancellationToken cancellationToken)
        {
            if (request.PetDto.Birthdate.Kind != DateTimeKind.Utc)
            {
                request.PetDto.Birthdate = DateTime.SpecifyKind(request.PetDto.Birthdate, DateTimeKind.Utc);
            }

            var pet = mapper.Map<Pet>(request.PetDto);
            context.Pets.Add(pet);
            await context.SaveChangesAsync(cancellationToken);
            return pet.Id;
        }
    }
}
