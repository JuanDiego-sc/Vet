using System;
using System.Runtime.InteropServices;
using Application.Core;
using Application.Interfaces;
using MediatR;
using Persistence;

namespace Application.Pets.Commands;

public class DeletePhoto
{
    public class Command : IRequest<Result<Unit>>
    {
        public required string PhotoId { get; set; }

    }

    public class Hanlder(AppDbContext dbContext, IPhotoService photoService)
        : IRequestHandler<Command, Result<Unit>>
    {
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var photo = dbContext.Photos.FirstOrDefault(x => x.Id == request.PhotoId);

            if (photo == null) return Result<Unit>.Failure("Failed to find the photo in DB", 400);

            await photoService.DeletePhoto(photo.PublicId);

            var result = await dbContext.SaveChangesAsync(cancellationToken) > 0;
    
            return result
                ? Result<Unit>.Success(Unit.Value)
                : Result<Unit>.Failure("Problem deleting photo on DB", 400);
        }
    }
}
