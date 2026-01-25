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

namespace MeetingSystem.Application.Resources.UpdateMeetingRoom
{
    public sealed class UpdateMeetingRoomCommandHandler(IApplicationDbContext context,
        IFileStorageService fileStorageService) : ICommandHandler<UpdateMeetingRoomCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(UpdateMeetingRoomCommand command, CancellationToken cancellationToken)
        {
            var resource = await context.Resources
                .Include(r => r.Company)
                .FirstOrDefaultAsync(r => r.Id == command.ResourceId, cancellationToken);

            if (resource is not MeetingRoom room)
            {
                return Result.Failure<Guid>(Error.NotFound("Resource.NotFound", "Meeting room not found"));
            }

            if (room.Company?.ManagerId != command.UserId)
            {
                return Result.Failure<Guid>(Error.Failure("Resource.Unauthorized", "You do not own this resource"));
            }

            string? newImageUrl = null;
            if (command.Image is not null && command.Image.Length > 0)
            {
                var imgResult = await fileStorageService.UploadFile(FileType.RESOURCE_IMAGE, command.Image, room.CompanyId, room.Id);
                if (imgResult.IsSuccess)
                {
                    newImageUrl = imgResult.Data;
                }
            }

            room.UpdateDetails(command.Name, command.Description, command.PricePerHour, newImageUrl, command.Capacity);

            room.ClearFeatures();
            foreach (var feature in command.Features)
            {
                room.AddFeature(feature);
            }

            await context.SaveChangesAsync(cancellationToken);
            return room.Id;
        }
    }
}
