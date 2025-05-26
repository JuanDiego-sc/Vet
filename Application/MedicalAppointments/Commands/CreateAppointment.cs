using System;
using Domain;
using MediatR;
using Persistence;

namespace Application.MedicalAppointments.Commands;

public class CreateAppointment
{
    public class Command : IRequest<string>
    {
         public required MedicalAppointment MedicalAppointment {get; set;}
    }

    public class Handler(AppDbContext context) : IRequestHandler<Command, string>
    {
        public async Task<string> Handle(Command request, CancellationToken cancellationToken)
        {
            if (request.MedicalAppointment.AppointmentDate.Kind != DateTimeKind.Utc)
            {
                request.MedicalAppointment.AppointmentDate = DateTime.SpecifyKind(request.MedicalAppointment.AppointmentDate, DateTimeKind.Utc);
            }
            
           context.MedicalAppointments.Add(request.MedicalAppointment);
           await context.SaveChangesAsync(cancellationToken);
           return request.MedicalAppointment.Id;
        }
    }
}
