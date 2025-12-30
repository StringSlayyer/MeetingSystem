using MeetingSystem.Application.Abstractions.DTOs;
using MeetingSystem.Application.Abstractions.Services;
using MeetingSystem.Contracts.Files;
using MeetingSystem.Domain.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Infrastructure.Services
{
    public class FileStorageService : IFileStorageService
    {
        private readonly string _fileStoragePath;
        private readonly ILogger<FileStorageService> _logger;
        private readonly List<string> _allowedExtensions = [".jpg", ".jpeg", ".png", ".heic", ".heif", ".mp4", ".hevc"];
        public FileStorageService(IConfiguration configuration, ILogger<FileStorageService> logger)
        {
            _fileStoragePath = configuration["FILE_STORAGE_PATH"];
            _logger = logger;
        }
        public async Task<Result<string>> UploadFile(FileType fileType, IFormFile file, Guid? companyId = null, Guid? resourceId = null)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return Result.Failure<string>(FileStorageError.NoFile);
                }

                var extension = Path.GetExtension(file.FileName);
                _logger.LogInformation("dany extension {ext}", extension);
                if (!_allowedExtensions.Contains(extension))
                {
                    return Result.Failure<string>(FileStorageError.FileTypeNotAllowed);
                }


                var fileName = Guid.NewGuid() + extension;
                var generatedPath = GenerateFilePath(fileType, companyId, resourceId);
                var savedFilePath = await SaveFileAsync(generatedPath, fileName, file.OpenReadStream());


                return savedFilePath;
            }
            catch (Exception ex)
            {
                return Result.Failure<string>(Error.Failure("FileStorage.Exception",$"Exception: {ex.Message}"));
            }
        }

        private string GenerateFilePath(FileType fileType, Guid? companyId, Guid? resourceId)
        {

            return fileType switch
            {
                FileType.COMPANY_IMAGE => $"companies/{companyId}",
                FileType.RESOURCE_IMAGE => $"companies/{companyId}/resources/{resourceId}",
                _ => throw new ArgumentException("Unsupported File Type")
            };
        }

        public async Task<string> SaveFileAsync(string path, string fileName, Stream fileStream)
        {
            _fileStoragePath.Trim();
            Directory.CreateDirectory(_fileStoragePath);
            var directory = Path.Combine(_fileStoragePath, path);
            Directory.CreateDirectory(directory);

            var filePath = Path.Combine(directory, fileName);

            await using var stream = new FileStream(filePath, FileMode.Create);
            await fileStream.CopyToAsync(stream);

            var response = Path.Combine(path, fileName);

            return response;
        }

        public void DeleteFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public async Task<Result<ReturnFileDTO>> ReturnFile(string filePath)
        {
            var model = new ReturnFileDTO();
            var fullPath = Path.Combine(_fileStoragePath, filePath);

            if (!Directory.Exists(_fileStoragePath))
            {
                return Result.Failure<ReturnFileDTO>(FileStorageError.FolderDoesntExist);
            }
            try
            {
                if (!File.Exists(fullPath))
                {
                    return Result.Failure<ReturnFileDTO>(FileStorageError.FileDoesntExist);
                }


                model.FileStream = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
                model.ContentType = GetContentType(fullPath);

                return model;
            }
            catch (Exception ex)
            {
                return Result.Failure<ReturnFileDTO>(Error.Failure("FileStorage.Exception", $"Exception {ex}"));
            }


        }

        private string GetContentType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(path, out var contentType))
            {
                contentType = "application/octet-stream"; // Default content type for unknown types
            }
            return contentType;
        }


    }
}
