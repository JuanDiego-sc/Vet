using System;
using System.Runtime.CompilerServices;
using Domain;
using MediatR;
using Persistence;

namespace Application.Diseases.Queries;

public class GetDisease
{
    public class Query : IRequest<Disease>{
        public required string Id {get; set;}
    }

    public class Handler(AppDbContext context) : IRequestHandler<Query, Disease>
    {
        public async Task<Disease> Handle(Query request, CancellationToken cancellationToken)
        {
            var disease = await context.Diseases.FindAsync([request.Id], cancellationToken);
            if (disease == null) throw new Exception("Disease not found");
            return disease;
        }
    }
}
