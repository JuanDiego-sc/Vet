using System;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.MedicalAppointments.Commands;

public class EditAppointment
{
    public class Command : IRequest{
        public required MedicalAppointment MedicalAppointment {get; set;}

        public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var detail = await context.Diseases
                .FindAsync([request.MedicalAppointment.Id], cancellationToken) 
             ?? 
            throw new Exception("Appointment not found");

            mapper.Map(request.MedicalAppointment, detail); 
            await context.SaveChangesAsync(cancellationToken);
        }
    }
    }
}
