using MeetingSystem.Client.Models;
using MeetingSystem.Contracts;
using MeetingSystem.Contracts.Companies;
using Microsoft.AspNetCore.Components.Forms;
using SharedKernel;

namespace MeetingSystem.Client.Abstractions
{
    public interface ICompanyService
    {
        Task<Result<PagedResult<CompanyDTO>>> GetCompaniesAsync(int page, int pageSize, string? searchTerm = null);
        Task<Result<string>> CreateCompanyAsync(CreateCompanyInputModel model, IBrowserFile? image);
        Task<Result<SingleCompanyDTO>> GetCompanyByIdAsync(string companyId);
        Task<Result<PagedResult<CompanyDTO>>> GetCompaniesByUserAsync(int page, int pageSize);
        Task<Result<string>> UpdateCompanyAsync(Guid companyId, CreateCompanyInputModel model, IBrowserFile? image);
    }
}
