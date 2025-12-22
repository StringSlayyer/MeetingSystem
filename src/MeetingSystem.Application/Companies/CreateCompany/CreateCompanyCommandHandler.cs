using MeetingSystem.Application.Abstractions.Data;
using MeetingSystem.Application.Abstractions.Messaging;
using MeetingSystem.Domain.Companies;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Companies.CreateCompany
{
    public sealed class CreateCompanyCommandHandler(IApplicationDbContext context)
        : ICommandHandler<CreateCompanyCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateCompanyCommand command, CancellationToken cancellationToken)
        {
            var address = new Address(command.Street, command.Number, command.City, command.State);
            var company = new Company(command.ManagerId, command.Name, address);

            await context.Companies.AddAsync(company, cancellationToken);
            await context.SaveChangesAsync();
            return company.Id;
        }
    }
}
