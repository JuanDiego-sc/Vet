using System;
using Application.DTOs;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Diseases.Commands;

public class CreateDisease
{
    public class Command : IRequest<string>
    {
         public required DiseaseDto DiseaseDto {get; set;}
    }

    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Command, string>
    {
        public async Task<string> Handle(Command request, CancellationToken cancellationToken)
        {
            var disease = mapper.Map<Disease>(request.DiseaseDto);
            context.Diseases.Add(disease);
            await context.SaveChangesAsync(cancellationToken);
            return disease.Id;
        }
    }
}
