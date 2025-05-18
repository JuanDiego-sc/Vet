using System;
using Application.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Pets.Queries;

public class GetPetList
{
    public class Query : IRequest<List<PetDto>>{}

    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Query, List<PetDto>>
    {
        public async Task<List<PetDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await
            context.Pets
            .Include(p => p.MedicalAppointments)
            .ProjectTo<PetDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        }
    }
}
