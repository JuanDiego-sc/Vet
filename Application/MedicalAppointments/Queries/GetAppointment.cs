using System;
using Application.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.MedicalAppointments.Queries;

public class GetAppointment
{
    public class Query : IRequest<MedicalAppointmentDto>{
        public required string Id {get; set;}
    }

    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Query, MedicalAppointmentDto>
    {
        public async Task<MedicalAppointmentDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var appointment =
            await context.MedicalAppointments
            .ProjectTo<MedicalAppointmentDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (appointment == null) throw new Exception("Appointment not found");
            return appointment;
        }
    }
}
