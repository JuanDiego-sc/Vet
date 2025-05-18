using System;
using Application.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Diseases.Queries;

public class GetDiseaseList
{
    public class Query : IRequest<List<DiseaseDto>>{}

    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Query, List<DiseaseDto>>
    {
        public async Task<List<DiseaseDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await context.Diseases
            .ProjectTo<DiseaseDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        }
    }
}
