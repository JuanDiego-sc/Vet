using System;
using Domain;
using MediatR;
using Persistence;

namespace Application.Medicines.Commands;

public class CreateMedicine
{
    public class Command : IRequest<string>
    {
         public required Medicine Medicine {get; set;}
    }

    public class Handler(AppDbContext context) : IRequestHandler<Command, string>
    {
        public async Task<string> Handle(Command request, CancellationToken cancellationToken)
        { 
           context.Medicines.Add(request.Medicine);
           await context.SaveChangesAsync(cancellationToken);
           return request.Medicine.Id;
        }
    }
}
