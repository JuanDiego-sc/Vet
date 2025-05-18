using System;
using Application.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Medicines.Queries;

public class GetMedicineList
{
    public class Query : IRequest<List<MedicineDto>>{}

    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Query, List<MedicineDto>>
    {
        public async Task<List<MedicineDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            return
            await context.Medicines
            .ProjectTo<MedicineDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        }
    }
}
