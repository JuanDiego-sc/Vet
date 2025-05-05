using System;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.AppointmentDetails.Queries;

public class GetDetailList
{
    public class Query : IRequest<List<AppointmentDetail>>{}

    public class Handler(AppDbContext context) : IRequestHandler<Query, List<AppointmentDetail>>{
        public async Task<List<AppointmentDetail>> Handle(Query request, CancellationToken cancellationToken){

            return await context.AppointmentDetails.ToListAsync(cancellationToken);
            
        }
    }
}
