using Microsoft.AspNetCore.Components.Forms;

namespace MeetingSystem.Client.Models
{
    public enum ResourceType
    {
        MeetingRoom,
        ParkingSpot
    }

    public class AddResourceInputModel
    {
        public string Name { get; set; } = "";
        public Guid CompanyId { get; set; }
        public string? Description { get; set; } = "";
        public ResourceType Type { get; set; } = ResourceType.MeetingRoom;
        public decimal PricePerHour { get; set; } = 0m; 

        public int Capacity { get; set; } = 1;
        public List<string> Features { get; set; } = new List<string>();
        public string TempFeatureInput { get; set; } = "";

        public bool IsCovered { get; set; }
    }
}
