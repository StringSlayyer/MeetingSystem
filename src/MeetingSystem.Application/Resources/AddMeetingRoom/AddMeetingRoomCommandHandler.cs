using MeetingSystem.Application.Abstractions.Data;
using MeetingSystem.Application.Abstractions.Messaging;
using MeetingSystem.Application.Abstractions.Services;
using MeetingSystem.Application.Companies.GetCompanies;
using MeetingSystem.Contracts.Files;
using MeetingSystem.Domain.Companies;
using MeetingSystem.Domain.Resources;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Resources.AddMeetingRoom
{
    public sealed class AddMeetingRoomCommandHandler(IApplicationDbContext context, IFileStorageService fileStorageService)
        : ICommandHandler<AddMeetingRoomCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(AddMeetingRoomCommand command, CancellationToken cancellationToken)
        {
            var company = await context.Companies.Where(c => c.Id == command.CompanyId).FirstOrDefaultAsync();
            if (company == null) return Result.Failure<Guid>(CompanyError.CompanyNotFound(command.CompanyId));

            

            if (company.ManagerId != command.UserId) return Result.Failure<Guid>(ResourceError.UserNotOwner);

            var meetingRoom = new MeetingRoom(command.Name, command.CompanyId, command.Description, command.PricePerHour, null, command.Capacity);

            if(command.Features.Count > 0)
            {
                foreach (string feature in command.Features) meetingRoom.AddFeature(feature);
            }

            if (command.Image != null && command.Image.Length > 0)
            {
                var imageResponse = await fileStorageService.UploadFile(FileType.RESOURCE_IMAGE, command.Image, command.CompanyId, meetingRoom.Id);
                meetingRoom.ImageUrl = imageResponse.IsSuccess ? imageResponse.Data : null;
            }

            company.AddRoom(meetingRoom);
            context.Resources.Entry(meetingRoom).State = EntityState.Added;
            await context.SaveChangesAsync(cancellationToken);
            return meetingRoom.Id;
        }
    }
}
