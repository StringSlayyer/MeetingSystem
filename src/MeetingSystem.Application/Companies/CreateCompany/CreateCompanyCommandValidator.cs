using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeetingSystem.Application.Companies.CreateCompany
{
    public sealed class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
    {
        public CreateCompanyCommandValidator()
        {
            RuleFor(x => x.ManagerId).NotEmpty();

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Company Name is required.")
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .MaximumLength(1000);

            RuleFor(x => x.Street).NotEmpty().MaximumLength(100);
            RuleFor(x => x.City).NotEmpty().MaximumLength(50);
            RuleFor(x => x.State).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Number).NotEmpty().MaximumLength(20);

            RuleFor(x => x.Image)
                .Must(BeAValidImage).WithMessage("Image must be a JPG or PNG and under 5MB.")
                .When(x => x.Image != null);
        }

        private bool BeAValidImage(IFormFile? file)
        {
            if (file is null) return true;
            if (file.Length > 5 * 1024 * 1024) return false; 

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var ext = System.IO.Path.GetExtension(file.FileName).ToLowerInvariant();
            return allowedExtensions.Contains(ext);
        }
    }
}
