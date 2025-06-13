using System;
using Application.DTOs;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.MedicalAppointments.Commands;

public class CreateAppointment
{
    public class Command : IRequest<string>
    {
         public required MedicalAppointmentDto MedicalAppointmentDto {get; set;}
    }

    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Command, string>
    {
        public async Task<string> Handle(Command request, CancellationToken cancellationToken)
        {
            if (request.MedicalAppointmentDto.AppointmentDate.Kind != DateTimeKind.Utc)
            {
                request.MedicalAppointmentDto.AppointmentDate = DateTime.SpecifyKind(request.MedicalAppointmentDto.AppointmentDate, DateTimeKind.Utc);
            }

            var appointment = mapper.Map<MedicalAppointment>(request.MedicalAppointmentDto);
            context.MedicalAppointments.Add(appointment);
            await context.SaveChangesAsync(cancellationToken);
            return appointment.Id;
        }
    }
}
