using System;
using Application.Core;
using Application.DTOs;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Pets.Commands;

public class EditPet
{
    public class Command : IRequest<Result<Unit>>
    {
        public required PetDto PetDto { get; set; }

        public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Command, Result<Unit>>
        {
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var pet = await context.Pets
                    .FindAsync([request.PetDto.Id], cancellationToken);

                if (pet == null) return Result<Unit>.Failure("Pet not found", 404);

                mapper.Map(request.PetDto, pet);
                var result = await context.SaveChangesAsync(cancellationToken) > 0;

                if (!result) return Result<Unit>.Failure("Failure to update the pet", 400);

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
