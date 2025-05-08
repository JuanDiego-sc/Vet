using System;
using Domain;
using MediatR;
using Persistence;

namespace Application.Medicines.Queries;

public class GetMedicine
{
    public class Query : IRequest<Medicine>{
        public required string Id {get; set;}
    }

    public class Handler(AppDbContext context) : IRequestHandler<Query, Medicine>
    {
        public async Task<Medicine> Handle(Query request, CancellationToken cancellationToken)
        {
            var medicine = await context.Medicines.FindAsync([request.Id], cancellationToken);
            if (medicine == null) throw new Exception("Appointment not found");
            return medicine;
        }
    }
}
