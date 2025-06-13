using System;
using Application.DTOs;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.MedicalAppointments.Commands;

public class EditAppointment
{
    public class Command : IRequest{
        public required MedicalAppointmentDto MedicalAppointmentDto {get; set;}

        public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var detail = await context.MedicalAppointments
                .FindAsync([request.MedicalAppointmentDto.Id], cancellationToken) 
             ?? 
            throw new Exception("Appointment not found");

            mapper.Map(request.MedicalAppointmentDto, detail); 
            await context.SaveChangesAsync(cancellationToken);
        }
    }
    }
}
