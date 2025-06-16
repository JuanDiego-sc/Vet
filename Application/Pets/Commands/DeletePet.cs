using System;
using Application.Core;
using MediatR;
using Persistence;

namespace Application.Pets.Commands;

public class DeletePet
{
    public class Command : IRequest<Result<Unit>>
    {
        public required string Id { get; set; }
    }

    public class Handler(AppDbContext context) : IRequestHandler<Command, Result<Unit>>
    {
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var pet = await context.Pets
                .FindAsync([request.Id], cancellationToken);

            if (pet == null) return Result<Unit>.Failure("Pet not found", 404);

            context.Remove(pet);
            var result = await context.SaveChangesAsync(cancellationToken) > 0;

            if (!result) return Result<Unit>.Failure("An error ocurred during delete the pet", 400);

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
