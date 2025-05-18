using System;
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
    public class Query : IRequest<MedicineDto>{
        public required string Id {get; set;}
    }

    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Query, MedicineDto>
    {
        public async Task<MedicineDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var medicine =
            await context.Medicines
            .ProjectTo<MedicineDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (medicine == null) throw new Exception("Appointment not found");
            return medicine;
        }
    }
}
