using MeetingSystem.Application.Abstractions.Data;
using MeetingSystem.Application.Abstractions.Messaging;
using MeetingSystem.Application.Abstractions.Services;
using MeetingSystem.Contracts.Files;
using MeetingSystem.Domain.Resources;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Resources.UpdateParkingSpot
{
    public sealed class UpdateParkingSpotCommandHandler(IApplicationDbContext context,
        IFileStorageService fileStorageService) : ICommandHandler<UpdateParkingSpotCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(UpdateParkingSpotCommand command, CancellationToken cancellationToken)
        {
            var resource = await context.Resources
                .Include(r => r.Company)
                .FirstOrDefaultAsync(r => r.Id == command.ResourceId, cancellationToken);

            if (resource is not ParkingSpot spot)
            {
                return Result.Failure<Guid>(ResourceError.ParkingSpotNotFound);
            }

            if (spot.Company?.ManagerId != command.UserId)
            {
                return Result.Failure<Guid>(ResourceError.Unauthorized);
            }

            string? newImageUrl = null;
            string? oldImageUrl = spot.ImageUrl;

            if (command.Image is not null && command.Image.Length > 0)
            {
                var uploadResult = await fileStorageService.UploadFile(
                    FileType.RESOURCE_IMAGE,
                    command.Image,
                    spot.CompanyId,
                    spot.Id);

                if (!uploadResult.IsSuccess)
                {
                    return Result.Failure<Guid>(uploadResult.Error);
                }

                newImageUrl = uploadResult.Data;
            }

            spot.UpdateDetails(command.Name, command.Description, command.PricePerHour,
                newImageUrl ?? spot.ImageUrl, command.IsCovered);

            await context.SaveChangesAsync(cancellationToken);

            if (newImageUrl is not null && !string.IsNullOrEmpty(oldImageUrl))
            {
                await fileStorageService.DeleteFileAsync(oldImageUrl);
            }

            return spot.Id;
        }
    
    }
}
