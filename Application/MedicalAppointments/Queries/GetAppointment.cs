using System;
using Domain;
using MediatR;
using Persistence;

namespace Application.MedicalAppointments.Queries;

public class GetAppointment
{
    public class Query : IRequest<MedicalAppointment>{
        public required string Id {get; set;}
    }

    public class Handler(AppDbContext context) : IRequestHandler<Query, MedicalAppointment>
    {
        public async Task<MedicalAppointment> Handle(Query request, CancellationToken cancellationToken)
        {
            var appointment = await context.MedicalAppointments.FindAsync([request.Id], cancellationToken);
            if (appointment == null) throw new Exception("Appointment not found");
            return appointment;
        }
    }
}
