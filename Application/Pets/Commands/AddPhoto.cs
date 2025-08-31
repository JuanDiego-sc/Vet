using System;
using Application.Core;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Persistence;

namespace Application.Pets.Commands;

public class AddPhoto
{
    public class Command : IRequest<Result<Photo>>
    {
        public required string PetId { get; set; }
        public required IFormFile File { get; set; }
    }

    //TODO: If the photo already exist, delete the prev photo and replace with the newest 
    public class Handler(AppDbContext dbContext, IPhotoService photoService)
        : IRequestHandler<Command, Result<Photo>>
    {
        public async Task<Result<Photo>> Handle(Command request, CancellationToken cancellationToken)
        {
            var pet = dbContext.Pets.FirstOrDefault(x => x.Id == request.PetId);

            if (pet == null) return Result<Photo>.Failure("Failed to find the entity in DB", 400);

            var uploadResult = await photoService.UploadPhoto(request.File);

            var photo = new Photo
            {
                PublicId = uploadResult.PublicId,
                Url = uploadResult.Url
            };

            pet.ImageUrl = uploadResult.Url;
            dbContext.Photos.Add(photo);

            var result = await dbContext.SaveChangesAsync(cancellationToken) > 0;

            return result
               ? Result<Photo>.Success(photo)
               : Result<Photo>.Failure("Problem saving photo on DB", 400);
        }
    }
}
