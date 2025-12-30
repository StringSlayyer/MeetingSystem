using MeetingSystem.Application.Abstractions.Data;
using MeetingSystem.Application.Abstractions.Messaging;
using MeetingSystem.Application.Abstractions.Services;
using MeetingSystem.Contracts.Files;
using MeetingSystem.Domain.Companies;
using MeetingSystem.Domain.Resources;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Companies.CreateCompany
{
    public sealed class CreateCompanyCommandHandler(IApplicationDbContext context, IFileStorageService fileStorageService)
        : ICommandHandler<CreateCompanyCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateCompanyCommand command, CancellationToken cancellationToken)
        {
            var address = new Address(command.Street, command.Number, command.City, command.State);
            var company = new Company(command.ManagerId, command.Name, command.Description, null, address);

            if (command.Image != null && command.Image.Length > 0)
            {
                var imageResponse = await fileStorageService.UploadFile(FileType.RESOURCE_IMAGE, command.Image, companyId:company.Id);
                company.ImageUrl = imageResponse.IsSuccess ? imageResponse.Data : null;
            }

            await context.Companies.AddAsync(company, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return company.Id;
        }
    }
}
