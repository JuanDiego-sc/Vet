using System;
using Application.Core;
using Application.DTOs;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Diseases.Commands;

public class CreateDisease
{
    public class Command : IRequest<Result<string>>
    {
         public required DiseaseDto DiseaseDto {get; set;}
    }

    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Command, Result<string>>
    {
        public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
        {
            var disease = mapper.Map<Disease>(request.DiseaseDto);
            context.Diseases.Add(disease);

            var result = await context.SaveChangesAsync(cancellationToken) > 0;
            if (!result) return Result<string>.Failure("Failure to create disease", 400);

            return Result<string>.Success(disease.Id);
        }
    }
}
