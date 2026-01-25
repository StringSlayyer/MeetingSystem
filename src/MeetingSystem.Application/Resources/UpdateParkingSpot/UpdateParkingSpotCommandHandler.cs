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
                return Result.Failure<Guid>(Error.NotFound("Resource.NotFound", "Parking spot not found"));
            }

            if (spot.Company?.ManagerId != command.UserId)
            {
                return Result.Failure<Guid>(Error.Failure("Resource.Unauthorized", "You do not own this resource"));
            }

            string? newImageUrl = null;
            if (command.Image is not null && command.Image.Length > 0)
            {
                var imgResult = await fileStorageService.UploadFile(FileType.RESOURCE_IMAGE, command.Image, spot.CompanyId, spot.Id);
                if (imgResult.IsSuccess)
                {
                    newImageUrl = imgResult.Data;
                }
            }

            spot.UpdateDetails(command.Name, command.Description, command.PricePerHour, newImageUrl, command.IsCovered);

            await context.SaveChangesAsync(cancellationToken);
            return spot.Id;
        }
    
    }
}
