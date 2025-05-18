using System;
using Application.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.AppointmentDetails.Queries;

public class GetDetailList
{
    public class Query : IRequest<List<AppointmentDetailDto>>{}

    public class Handler(AppDbContext context, IMapper mapper ) : IRequestHandler<Query, List<AppointmentDetailDto>>{
        public async Task<List<AppointmentDetailDto>> Handle(Query request, CancellationToken cancellationToken){

            return await context.AppointmentDetails
            .Include(d => d.Disease)
            .Include(d => d.MedicalAppointment)
            .Include(d => d.Treatments)
                .ThenInclude(t => t.Medicine)
            .ProjectTo<AppointmentDetailDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
            
        }
    }
}
