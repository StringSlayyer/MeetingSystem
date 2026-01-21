using MeetingSystem.Contracts.Resources;

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

    public sealed record SingleCompanyDTO(
        Guid Id,
        string Name,
        string Description,
        string ImageUrl,
        ManagerDTO Manager,
        string Number,
        string Street,
        string City,
        string State,
        double Rating,
        int BookingCount,
        List<ResourceDTO> Resources
        );

    public sealed record ManagerDTO(   
        Guid ManagerId,
        string FirstName,
        string LastName
        );


}