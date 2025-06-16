using System;
using Application.Core;
using MediatR;
using Persistence;

namespace Application.Medicines.Commands;

public class DeleteMedicine
{
    public class Command : IRequest<Result<Unit>>
    {
         public required string Id { get; set; }
    }

    public class Handler(AppDbContext context) : IRequestHandler<Command, Result<Unit>>
    {
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var medicine = await context.Medicines
                .FindAsync([request.Id], cancellationToken);

            if (medicine == null) return Result<Unit>.Failure("Medicine not found", 404);

            context.Remove(medicine);
            var result = await context.SaveChangesAsync(cancellationToken) > 0;

            if (!result) return Result<Unit>.Failure("Failure to delete medicine", 400);

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
