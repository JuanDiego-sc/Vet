using System;
using Application.Core;
using Application.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Pets.Queries;

public class GetPet
{
    public class Query : IRequest<Result<PetDto>>{
        public required string Id {get; set;}
    }

    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Query, Result<PetDto>>
    {
        public async Task<Result<PetDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var pet =
            await context.Pets
            .Include(p => p.MedicalAppointments)
            .ProjectTo<PetDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (pet == null) return Result<PetDto>.Failure("Pet not found", 404);
            
            return Result<PetDto>.Success(pet);
        }
    }
}
 
