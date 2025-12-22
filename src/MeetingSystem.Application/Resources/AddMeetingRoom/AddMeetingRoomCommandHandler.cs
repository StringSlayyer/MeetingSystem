using MeetingSystem.Application.Abstractions.Data;
using MeetingSystem.Application.Abstractions.Messaging;
using MeetingSystem.Application.Companies.GetCompanies;
using MeetingSystem.Domain.Companies;
using MeetingSystem.Domain.Resources;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Resources.AddMeetingRoom
{
    public sealed class AddMeetingRoomCommandHandler(IApplicationDbContext context)
        : ICommandHandler<AddMeetingRoomCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(AddMeetingRoomCommand command, CancellationToken cancellationToken)
        {
            var company = await context.Companies.Where(c => c.Id == command.CompanyId).FirstOrDefaultAsync();
            if (company == null) return Result.Failure<Guid>(CompanyError.CompanyNotFound(command.CompanyId));

            if (company.ManagerId != command.UserId) return Result.Failure<Guid>(ResourceError.UserNotOwner);

            var meetingRoom = new MeetingRoom(command.Name, command.CompanyId, command.Seats);
            await context.Resources.AddAsync(meetingRoom, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return meetingRoom.Id;
        }
    }
}
