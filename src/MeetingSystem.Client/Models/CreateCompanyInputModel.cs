namespace MeetingSystem.Client.Models
{
    public class CreateCompanyInputModel
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string Street { get; set; } = "";
        public string Number { get; set; } = "";
        public string City { get; set; } = "";
        public string State { get; set; } = "";
    }
}
