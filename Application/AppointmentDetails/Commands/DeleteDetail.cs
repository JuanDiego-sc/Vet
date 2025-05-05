using System;
using MediatR;
using Persistence;

namespace Application.AppointmentDetails.Commands;

public class DeleteDetail
{
    public class Command : IRequest{
        public required string Id {get; set;}
    }

    public class Handler(AppDbContext context) : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var detail = await context.AppointmentDetails
                .FindAsync([request.Id], cancellationToken) 
             ?? 
            throw new Exception("Appointment detail not found");

            context.Remove(detail); 
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
