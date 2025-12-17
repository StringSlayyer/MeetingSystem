using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Domain.Resources
{
    public sealed class MeetingRoom : Resource
    {
        public int Seats { get; set; }
    }
}
