using MeetingSystem.Application.Abstractions.Data;
using MeetingSystem.Domain.Reservations.Events;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Reservations.Events
{
    internal class ReservationCreatedDomainEventHandler(
        IApplicationDbContext context)
        : IDomainEventHandler<ReservationCreatedDomainEvent>
    {
        public async Task Handle(ReservationCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            var resource = await context.Resources
            .Include(r => r.Company)
            .FirstOrDefaultAsync(r => r.Id == domainEvent.ResourceId, cancellationToken);

            if (resource is null || resource.Company is null)
            {
                return;
            }

            resource.Company.IncrementBookingCount();

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
