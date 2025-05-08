using System;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.MedicalAppointments.Queries;

public class GetAppointmentList
{
    public class Query : IRequest<List<MedicalAppointment>>{}

    public class Handler(AppDbContext context) : IRequestHandler<Query, List<MedicalAppointment>>
    {
        public async Task<List<MedicalAppointment>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await context.MedicalAppointments.ToListAsync(cancellationToken);
        }
    }
}
