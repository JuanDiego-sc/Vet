using System;
using System.Security.Cryptography;
using Domain;
using MediatR;
using Persistence;

namespace Application.AppointmentDetails.Queries;

public class GetDetail
{
    public class Query : IRequest<AppointmentDetail>{
        public required string Id {get; set;}
    }

    public class Handler(AppDbContext context) : IRequestHandler<Query, AppointmentDetail>
    {
        public async Task<AppointmentDetail> Handle(Query request, CancellationToken cancellationToken)
        {
            var detail = await context.AppointmentDetails.FindAsync([request.Id], cancellationToken);
            if (detail == null) throw new Exception("Detail not found");
            return detail;
        }
    }
}
