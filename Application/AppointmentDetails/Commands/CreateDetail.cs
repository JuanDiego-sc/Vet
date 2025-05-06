using System;
using Domain;
using MediatR;
using Persistence;

namespace Application.AppointmentDetails.Commands;

public class CreateDetail
{
    public class Command : IRequest<string>
    {
        public required AppointmentDetail AppointmentDetail {get; set;}
    }

    public class Handler(AppDbContext context) : IRequestHandler<Command, string>
    {
        public async Task<string> Handle(Command request, CancellationToken cancellationToken)
        {
           context.AppointmentDetails.Add(request.AppointmentDetail);
           await context.SaveChangesAsync(cancellationToken);
           return request.AppointmentDetail.Id;
        }
    }
}
