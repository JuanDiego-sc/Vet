using System;
using Application.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Treatments.Queries;

public class GetTreatment
{
    public class Query : IRequest<TreatmentDto>{
        public required string Id {get; set;}
    }

    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Query, TreatmentDto>
    {
        public async Task<TreatmentDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var treatment = await context.Treatments
            .Include(t => t.Medicine)
            .Include(t => t.AppointmentDetail)
            .ProjectTo<TreatmentDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (treatment == null) throw new Exception("Pet not found");
            return treatment;
        }
    }
}
