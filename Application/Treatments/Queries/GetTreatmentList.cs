using System;
using Application.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Treatments.Queries;

public class GetTreatmentList
{
    public class Query : IRequest<List<TreatmentDto>>{}

    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Query, List<TreatmentDto>>
    {
        public async Task<List<TreatmentDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await context.Treatments
            .Include(t => t.Medicine)
            .Include(t => t.AppointmentDetail)
            .ProjectTo<TreatmentDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        }
    }
}
