using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Domain.Resources
{
    public sealed class MeetingRoom : Resource
    {
        public int Seats { get; private set; }
        public MeetingRoom() { }
        public MeetingRoom(string name, Guid companyId, int seats)
            : base(name, companyId)
        {
            this.Seats = seats;
        }
    }
}
