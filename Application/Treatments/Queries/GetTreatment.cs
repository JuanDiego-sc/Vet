using System;
using Domain;
using MediatR;
using Persistence;

namespace Application.Treatments.Queries;

public class GetTreatment
{
    public class Query : IRequest<Treatment>{
        public required string Id {get; set;}
    }

    public class Handler(AppDbContext context) : IRequestHandler<Query, Treatment>
    {
        public async Task<Treatment> Handle(Query request, CancellationToken cancellationToken)
        {
            var treatment = await context.Treatments.FindAsync([request.Id], cancellationToken);
            if (treatment == null) throw new Exception("Pet not found");
            return treatment;
        }
    }
}
