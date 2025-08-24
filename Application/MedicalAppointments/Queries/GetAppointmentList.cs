using System;
using Application.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.MedicalAppointments.Queries;

public class GetAppointmentList
{
    public class Query : IRequest<List<MedicalAppointmentDto>>{}

    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Query, List<MedicalAppointmentDto>>
    {
        public async Task<List<MedicalAppointmentDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await context.MedicalAppointments
            .ProjectTo<MedicalAppointmentDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        }
    }
}
