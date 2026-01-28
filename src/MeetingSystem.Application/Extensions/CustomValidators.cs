using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Extensions
{
    public static class CustomValidators
    {
        public static IRuleBuilderOptions<T, IFormFile?> ValidImage<T>(this IRuleBuilder<T, IFormFile?> ruleBuilder)
        {
            return ruleBuilder
                .Must(file =>
                {
                    if (file is null) return true;
                    if (file.Length > 5 * 1024 * 1024) return false;

                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                    var ext = System.IO.Path.GetExtension(file.FileName).ToLowerInvariant();
                    return allowedExtensions.Contains(ext);
                })
                .WithMessage("Image must be a JPG or PNG and under 5MB.");
        }
    }
}
