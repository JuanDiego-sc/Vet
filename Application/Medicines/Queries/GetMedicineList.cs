using System;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Medicines.Queries;

public class GetMedicineList
{
    public class Query : IRequest<List<Medicine>>{}

    public class Handler(AppDbContext context) : IRequestHandler<Query, List<Medicine>>
    {
        public async Task<List<Medicine>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await context.Medicines.ToListAsync(cancellationToken);
        }
    }
}
