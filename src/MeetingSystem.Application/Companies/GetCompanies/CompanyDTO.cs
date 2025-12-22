namespace MeetingSystem.Application.Companies.GetCompanies
{
    public sealed record CompanyDTO(Guid Id, Guid ManagerId, string Name, string Number, string Street, string City, string State, int ResourceCount );
}