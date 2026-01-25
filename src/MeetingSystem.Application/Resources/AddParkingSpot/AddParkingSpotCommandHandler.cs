using MeetingSystem.Application.Abstractions.Data;
using MeetingSystem.Application.Abstractions.Messaging;
using MeetingSystem.Application.Abstractions.Services;
using MeetingSystem.Contracts.Files;
using MeetingSystem.Domain.Companies;
using MeetingSystem.Domain.Resources;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Resources.AddParkingSpot
{
    public sealed class AddParkingSpotCommandHandler(IApplicationDbContext context, IFileStorageService fileStorageService)
        : ICommandHandler<AddParkingSpotCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(AddParkingSpotCommand command, CancellationToken cancellationToken)
        {
            var company = await context.Companies.Where(c => c.Id == command.CompanyId).FirstOrDefaultAsync();
            if (company == null) return Result.Failure<Guid>(CompanyError.CompanyNotFound(command.CompanyId));

            if (company.ManagerId != command.UserId) return Result.Failure<Guid>(ResourceError.UserNotOwner);

            var parkingSpot = new ParkingSpot(company.Name, command.CompanyId, command.Description, command.PricePerHour, null, command.IsCovered);

            if(command.Image != null && command.Image.Length > 0)
            {
                var imageResponse = await fileStorageService.UploadFile(FileType.RESOURCE_IMAGE, command.Image, command.CompanyId, parkingSpot.Id);
                parkingSpot.ImageUrl = imageResponse.IsSuccess ? imageResponse.Data : null;
            }

            company.AddRoom(parkingSpot);
            context.Resources.Entry(parkingSpot).State = EntityState.Added;

            await context.SaveChangesAsync(cancellationToken);
            return parkingSpot.Id;
        }
    }
}
