namespace MeetingSystem.Contracts.Companies
{
    public sealed record CompanyDTO(
        Guid Id,
        string Name,
        string Description,      
        string ImageUrl,
        string City,
        string State,
        double Rating,           
        int BookingCount
        );
}