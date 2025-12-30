using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Abstractions.DTOs
{
    public class ReturnFileDTO
    {
        public FileStream? FileStream { get; set; }
        public string? ContentType { get; set; }
        public string? FileUrl { get; set; }
    }
}
