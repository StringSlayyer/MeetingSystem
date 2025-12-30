using MeetingSystem.Application.Abstractions.Messaging;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Resources.AddMeetingRoom
{
    public sealed record AddMeetingRoomCommand(Guid UserId, string Name, Guid CompanyId,
        string Description, decimal PricePerHour, IFormFile? Image, int Capacity,
        List<string> Features) : ICommand<Guid>;
}
