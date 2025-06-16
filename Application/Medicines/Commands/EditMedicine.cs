using System;
using Application.Core;
using Application.DTOs;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Medicines.Commands;

public class EditMedicine
{
    public class Command : IRequest<Result<Unit>>
    {
        public required MedicineDto MedicineDto {get; set;}

        public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Command, Result<Unit>>
        {
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var medicine = await context.Medicines
                    .FindAsync([request.MedicineDto.Id], cancellationToken);

                if (medicine == null) return Result<Unit>.Failure("Medicine not found", 404);

                mapper.Map(request.MedicineDto, medicine);
                var result = await context.SaveChangesAsync(cancellationToken) > 0;

                if (!result) return Result<Unit>.Failure("Failure to edit medicine", 400);

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
