using System;
using System.Runtime.CompilerServices;
using Application.Core;
using Application.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Diseases.Queries;

public class GetDisease
{
    public class Query : IRequest<Result<DiseaseDto>>{
        public required string Id {get; set;}
    }

    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Query, Result<DiseaseDto>>
    {
        public async Task<Result<DiseaseDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var disease =
            await context.Diseases
            .ProjectTo<DiseaseDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (disease == null) return Result<DiseaseDto>.Failure("Disease not found", 404);

            return Result<DiseaseDto>.Success(disease);
        }
    }
}
