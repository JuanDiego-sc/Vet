using System;
using Application.Core;
using Application.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Medicines.Queries;

public class GetMedicine
{
    public class Query : IRequest<Result<MedicineDto>>{
        public required string Id {get; set;}
    }

    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Query, Result<MedicineDto>>
    {
        public async Task<Result<MedicineDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var medicine =
            await context.Medicines
            .ProjectTo<MedicineDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (medicine == null) return Result<MedicineDto>.Failure("Medicine not found", 404);
            return Result<MedicineDto>.Success(medicine);
        }
    }
}
