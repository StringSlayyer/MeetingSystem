using MeetingSystem.Application.Abstractions.Messaging;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MeetingSystem.Application.Resources.UpdateParkingSpot
{
    public sealed record UpdateParkingSpotCommand(
        Guid ResourceId,
        Guid UserId,
        string Name,
        string Description,
        decimal PricePerHour,
        IFormFile? Image,
        bool IsCovered) : ICommand<Guid>;
}
