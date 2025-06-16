using System;
using Application.Core;
using Application.DTOs;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Pets.Commands;

public class CreatePet
{
     public class Command : IRequest<Result<string>>
    {
        public required PetDto PetDto { get; set; }
    }

    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Command, Result<string>>
    {
        public async Task<Result<string> >Handle(Command request, CancellationToken cancellationToken)
        {
            if (request.PetDto.Birthdate.Kind != DateTimeKind.Utc)
            {
                request.PetDto.Birthdate = DateTime.SpecifyKind(request.PetDto.Birthdate, DateTimeKind.Utc);
            }

            var pet = mapper.Map<Pet>(request.PetDto);
            context.Pets.Add(pet);

            var result = await context.SaveChangesAsync(cancellationToken) > 0;

            if (!result) return Result<string>.Failure("Failure to create the pet", 400);
            
            return Result<string>.Success(pet.Id);
        }
    }
}
