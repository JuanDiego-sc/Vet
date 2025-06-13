using System;
using Application.DTOs;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Pets.Commands;

public class EditPet
{
    //TODO: Use DTOs for create, edit and delete request
    public class Command : IRequest
    {
        public required PetDto PetDto { get; set; }

        public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Command>
        {
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var pet = await context.Pets
                    .FindAsync([request.PetDto.Id], cancellationToken)
                 ??
                throw new Exception("Pet not found");

                mapper.Map(request.PetDto, pet);
                await context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
