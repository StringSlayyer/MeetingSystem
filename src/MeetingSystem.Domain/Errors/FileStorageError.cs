using SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Domain.Errors
{
    public static class FileStorageError
    {
        public static Error NoFile
            => Error.Failure("FileStorage.NoFile", "No file provided or file is empty");
        public static Error FileTypeNotAllowed
            => Error.Failure("FileStorage.FileTypeNotAllowed", "File type not allowed");
        public static Error FolderDoesntExist
            => Error.NotFound("FileStorage.FolderDoesntExist", "Folder does not exist");
        public static Error FileDoesntExist
            => Error.NotFound("FileStorage.FileDoesntExist", "File does not exist");

    }
}
