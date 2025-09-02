using System;
using Application.Core;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Persistence;

namespace Application.Medicines.Commands;

public class AddPhoto
{
    public class Command : IRequest<Result<Photo>>
    {
        public required string MedicineId { get; set; }
        public required IFormFile File { get; set; }
    }

    public class Handler(AppDbContext dbContext, IPhotoService photoService)
        : IRequestHandler<Command, Result<Photo>>
    {
        public async Task<Result<Photo>> Handle(Command request, CancellationToken cancellationToken)
        {
            var medicine = dbContext.Medicines.FirstOrDefault(x => x.Id == request.MedicineId);

            if (medicine == null) return Result<Photo>.Failure("Failed to find the entity in DB", 400);
            
            var photo = dbContext.Photos.FirstOrDefault(x => x.Url == medicine.ImageUrl);

            if (photo != null)
            {
                await photoService.DeletePhoto(photo.PublicId);
                medicine.ImageUrl = null;
            }

            var uploadResult = await photoService.UploadPhoto(request.File);

            var newPhoto = new Photo
            {
                PublicId = uploadResult.PublicId,
                Url = uploadResult.Url
            };

            medicine.ImageUrl = uploadResult.Url;
            dbContext.Photos.Add(newPhoto);

            var result = await dbContext.SaveChangesAsync(cancellationToken) > 0;

            return result
               ? Result<Photo>.Success(newPhoto)
               : Result<Photo>.Failure("Problem saving photo on DB", 400);
        }
    }
}
