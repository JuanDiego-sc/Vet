using System;
using Application.Core;
using MediatR;
using Persistence;

namespace Application.Diseases.Commands;

public class DeleteDisease
{
    public class Command : IRequest<Result<Unit>>
    {
        public required string Id {get; set;}
    }

    public class Handler(AppDbContext context) : IRequestHandler<Command, Result<Unit>>
    {
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var disease = await context.Diseases
                .FindAsync([request.Id], cancellationToken);

            if (disease == null) return Result<Unit>.Failure("Disease not found", 404);

            context.Remove(disease);
            var result = await context.SaveChangesAsync(cancellationToken) > 0;

            if (!result) return Result<Unit>.Failure("Failure to delete disease", 400);

            return Result<Unit>.Success(Unit.Value);

        }
    }
}
