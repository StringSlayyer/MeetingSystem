namespace MeetingSystem.Client.Models
{
    public class EditResourceInputModel
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal PricePerHour { get; set; }
        public ResourceType Type { get; set; } 

        public int Capacity { get; set; }
        public List<string> Features { get; set; } = new();

        public bool IsCovered { get; set; }
    }
}
