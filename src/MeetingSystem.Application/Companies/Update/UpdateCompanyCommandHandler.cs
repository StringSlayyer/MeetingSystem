using MeetingSystem.Application.Abstractions.Data;
using MeetingSystem.Application.Abstractions.Messaging;
using MeetingSystem.Application.Abstractions.Services;
using MeetingSystem.Contracts.Files;
using MeetingSystem.Domain.Companies;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Companies.Update
{
    public sealed class UpdateCompanyCommandHandler(
        IApplicationDbContext context, IFileStorageService fileStorageService)
        : ICommandHandler<UpdateCompanyCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(UpdateCompanyCommand command, CancellationToken cancellationToken)
        {
            var company = await context.Companies
                        .FirstOrDefaultAsync(c => c.Id == command.CompanyId, cancellationToken);

            if (company is null)
            {
                return Result.Failure<Guid>(CompanyError.CompanyNotFound(command.CompanyId));
            }

            if (company.ManagerId != command.ManagerId)
            {
                return Result.Failure<Guid>(CompanyError.Unauthorized);
            }

            string? newImageUrl = null;
            if (command.Image != null && command.Image.Length > 0)
            {
                var imageResponse = await fileStorageService.UploadFile(FileType.RESOURCE_IMAGE, command.Image, companyId: company.Id);
                if (imageResponse.IsSuccess)
                {
                    newImageUrl = imageResponse.Data;
                }
            }

            var newAddress = new Address(command.Street, command.Number, command.City, command.State);

            company.UpdateDetails(command.Name, command.Description, newAddress, newImageUrl);

            await context.SaveChangesAsync(cancellationToken);

            return company.Id;
        }
    }
}
