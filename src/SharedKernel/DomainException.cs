using System;
using System.Collections.Generic;
using System.Text;

namespace SharedKernel
{
    public class DomainException : Exception
    {
        public string Title { get; }
        public string Code { get; }

        public DomainException(string code, string message, string title = "Domain Error")
            : base(message)
        {
            Code = code;
            Title = title;
        }
    }
}
