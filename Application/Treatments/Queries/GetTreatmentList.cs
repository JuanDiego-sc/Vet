using System;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Treatments.Queries;

public class GetTreatmentList
{
    public class Query : IRequest<List<Treatment>>{}

    public class Handler(AppDbContext context) : IRequestHandler<Query, List<Treatment>>
    {
        public async Task<List<Treatment>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await context.Treatments.ToListAsync(cancellationToken);
        }
    }
}
