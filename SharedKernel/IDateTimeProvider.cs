using System;
using System.Collections.Generic;
using System.Text;

namespace SharedKernel
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}
