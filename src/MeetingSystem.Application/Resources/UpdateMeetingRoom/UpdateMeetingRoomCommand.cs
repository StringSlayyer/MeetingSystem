using MeetingSystem.Application.Abstractions.Messaging;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MeetingSystem.Application.Resources.UpdateMeetingRoom
{
    public sealed record UpdateMeetingRoomCommand(
        Guid ResourceId,
        Guid UserId,
        string Name,
        string Description,
        decimal PricePerHour,
        IFormFile? Image,
        int Capacity,
        List<string> Features) : ICommand<Guid>;
}
