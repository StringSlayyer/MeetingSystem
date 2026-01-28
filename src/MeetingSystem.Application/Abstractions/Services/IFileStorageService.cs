using MeetingSystem.Application.Abstractions.DTOs;
using MeetingSystem.Contracts.Files;
using Microsoft.AspNetCore.Http;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Abstractions.Services
{
    public interface IFileStorageService
    {
        Task<Result<string>> UploadFile(FileType fileType, IFormFile file, Guid? companyId = null, Guid? resourceId = null);
        Task<string> SaveFileAsync(string path, string fileName, Stream fileStream);
        Task<Result<ReturnFileDTO>> ReturnFile(string filePath);
        Task<Result> DeleteFileAsync(string relativeFilePath);
    }
}
