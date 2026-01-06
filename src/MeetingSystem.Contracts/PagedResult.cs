using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Contracts
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; } = new();
        public int TotalCount { get; set; }
    }
}
