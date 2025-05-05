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
            if (!string.IsNullOrEmpty(request.AppointmentDetail.IdDisease))
            {
                var disease = await context.Diseases.FindAsync(request.AppointmentDetail.IdDisease);
                if (disease is null) throw new Exception("Disease not found");
                request.AppointmentDetail.IdDisease = disease.ToString();
            }

           context.AppointmentDetails.Add(request.AppointmentDetail);
           await context.SaveChangesAsync(cancellationToken);
           return request.AppointmentDetail.Id;
        }
    }
}
