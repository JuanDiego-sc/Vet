using System;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Diseases.Commands;

public class EditDisease
{
    public class Command : IRequest{
        public required Disease Disease {get; set;}

        public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var detail = await context.Diseases
                .FindAsync([request.Disease.Id], cancellationToken) 
             ?? 
            throw new Exception("Appointment detail not found");

            mapper.Map(request.Disease, detail); 
            await context.SaveChangesAsync(cancellationToken);
        }
    }
    }
}
