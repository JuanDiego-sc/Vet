using System;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Pets.Queries;

public class GetPetList
{
    public class Query : IRequest<List<Pet>>{}

    public class Handler(AppDbContext context) : IRequestHandler<Query, List<Pet>>
    {
        public async Task<List<Pet>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await context.Pets.ToListAsync(cancellationToken);
        }
    }
}
