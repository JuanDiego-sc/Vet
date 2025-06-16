using System;
using Application.Core;
using Application.DTOs;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Diseases.Commands;

public class EditDisease
{
    public class Command : IRequest<Result<Unit>>
    {
        public required DiseaseDto DiseaseDto {get; set;}

        public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Command, Result<Unit>>
        {
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var disease = await context.Diseases
                    .FindAsync([request.DiseaseDto.Id], cancellationToken);

                if (disease == null) return Result<Unit>.Failure("Disease not found", 404);

                mapper.Map(request.DiseaseDto, disease);
                var result = await context.SaveChangesAsync(cancellationToken) > 0;

                if (!result) return Result<Unit>.Failure("Failure to edit a disease", 400);

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
