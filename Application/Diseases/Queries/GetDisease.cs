using System;
using System.Runtime.CompilerServices;
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
    public class Query : IRequest<DiseaseDto>{
        public required string Id {get; set;}
    }

    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Query, DiseaseDto>
    {
        public async Task<DiseaseDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var disease =
            await context.Diseases
            .ProjectTo<DiseaseDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (disease == null) throw new Exception("Disease not found");
            return disease;
        }
    }
}
