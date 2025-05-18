using System;
using System.Security.Cryptography;
using Application.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.AppointmentDetails.Queries;

public class GetDetail
{
    public class Query : IRequest<AppointmentDetailDto>{
        public required string Id {get; set;}
    }

    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Query, AppointmentDetailDto>
    {
        public async Task<AppointmentDetailDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var detail =
            await context.AppointmentDetails
            .Include(d => d.Disease)
            .Include(d => d.MedicalAppointment)
            .Include(d => d.Treatments)
                .ThenInclude(t => t.Medicine)
            .ProjectTo<AppointmentDetailDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (detail == null) throw new Exception("Detail not found");

            return detail;
        }
    }
}
