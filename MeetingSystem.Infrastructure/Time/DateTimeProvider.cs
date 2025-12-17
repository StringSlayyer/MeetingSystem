using System;
using System.Collections.Generic;
using System.Text;
using SharedKernel;

namespace MeetingSystem.Infrastructure.Time
{
    internal sealed class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
