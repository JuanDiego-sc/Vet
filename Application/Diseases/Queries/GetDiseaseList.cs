using System;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Diseases.Queries;

public class GetDiseaseList
{
    public class Query : IRequest<List<Disease>>{}

    public class Handler(AppDbContext context) : IRequestHandler<Query, List<Disease>>
    {
        public async Task<List<Disease>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await context.Diseases.ToListAsync(cancellationToken);
        }
    }
}
