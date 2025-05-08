using System;
using MediatR;
using Persistence;

namespace Application.MedicalAppointments.Commands;

public class DeleteAppointment
{
    public class Command : IRequest{
        public required string Id {get; set;}
    }

    public class Handler(AppDbContext context) : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var appointment = await context.MedicalAppointments
                .FindAsync([request.Id], cancellationToken) 
             ?? 
            throw new Exception("Appointment not found");

            context.Remove(appointment); 
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
