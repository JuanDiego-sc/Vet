using System;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.AppointmentDetails.Commands;

public class EditDetail
{
    public class Command : IRequest{
        public required AppointmentDetail AppointmentDetail {get; set;}
    }

    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var detail = await context.AppointmentDetails
                .FindAsync([request.AppointmentDetail.Id], cancellationToken) 
             ?? 
            throw new Exception("Appointment detail not found");

            mapper.Map(request.AppointmentDetail, detail); 
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
