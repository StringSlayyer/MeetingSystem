using SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Domain.Reservations
{
    public record TimeSlot
    {
        public DateTime Start { get; }
        public DateTime End { get; }
        public TimeSlot(DateTime start, DateTime end)
        {
            if (end <= start) throw new InvalidOperationException("End time must be after a start time");
            Start = start;
            End = end;
        }

        public bool OverlapsWith(TimeSlot other)
        {
            return Start < other.End && other.Start < End;
        }
    }
}
