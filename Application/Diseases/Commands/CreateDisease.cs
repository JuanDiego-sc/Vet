using System;
using Domain;
using MediatR;
using Persistence;

namespace Application.Diseases.Commands;

public class CreateDisease
{
    public class Command : IRequest<string>
    {
         public required Disease Disease {get; set;}
    }

    public class Handler(AppDbContext context) : IRequestHandler<Command, string>
    {
        public async Task<string> Handle(Command request, CancellationToken cancellationToken)
        {
           context.Diseases.Add(request.Disease);
           await context.SaveChangesAsync(cancellationToken);
           return request.Disease.Id;
        }
    }
}
