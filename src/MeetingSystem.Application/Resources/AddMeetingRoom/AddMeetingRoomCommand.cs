using MeetingSystem.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Resources.AddMeetingRoom
{
    public sealed record AddMeetingRoomCommand(Guid UserId, string Name, Guid CompanyId, int Seats) : ICommand<Guid>;
}
