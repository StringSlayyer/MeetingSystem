using MeetingSystem.Application.Abstractions.Messaging;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MeetingSystem.Application.Resources.AddParkingSpot
{
    public sealed record AddParkingSpotCommand(Guid UserId, string Name, Guid CompanyId,
        string Description, decimal PricePerHour, IFormFile? Image, int Capacity, bool IsCovered) : ICommand<Guid>;
}
