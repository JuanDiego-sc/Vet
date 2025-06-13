using System;
using Application.DTOs;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Treatments.Commands;

public class CreateTreatment
{
     public class Command : IRequest<string>
    {
         public required TreatmentDto TreatmentDto{get; set;}
    }

    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Command, string>
    {
        public async Task<string> Handle(Command request, CancellationToken cancellationToken)
        {
            var treatment = mapper.Map<Treatment>(request.TreatmentDto);
            context.Treatments.Add(treatment);
            await context.SaveChangesAsync(cancellationToken);
            return treatment.Id;
        }
    }
}
