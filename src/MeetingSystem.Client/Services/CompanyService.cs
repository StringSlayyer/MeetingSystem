using MeetingSystem.Client.Abstractions;
using MeetingSystem.Client.Extensions;
using MeetingSystem.Client.Models;
using MeetingSystem.Contracts;
using MeetingSystem.Contracts.Companies;
using Microsoft.AspNetCore.Components.Forms;
using SharedKernel;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace MeetingSystem.Client.Services
{
    public class CompanyService(HttpClient client) : ICompanyService
    {
        private readonly HttpClient _http = client;

        public async Task<Result<string>> CreateCompanyAsync(CreateCompanyInputModel model, IBrowserFile? image)
        {
            try
            {
                using var content = new MultipartFormDataContent();

                void AddString(string value, string name) =>
                    content.Add(new StringContent(value ?? string.Empty), name);

                AddString(model.Name, "Name");
                AddString(model.Description, "Description");
                AddString(model.Number, "Number");
                AddString(model.Street, "Street");
                AddString(model.City, "City");
                AddString(model.State, "State");

                if (image is not null)
                {
                    var maxFileSize = 5 * 1024 * 1024;

                    var fileContent = new StreamContent(image.OpenReadStream(maxFileSize));

                    fileContent.Headers.ContentType = new MediaTypeHeaderValue(image.ContentType);

                    content.Add(fileContent, "Image", image.Name);
                }

                var response = await _http.PostAsync("api/Company/create", content);

                if (!response.IsSuccessStatusCode)
                {
                    return await response.ToFailureResultAsync<string>("Client.Companies.Create");
                }

                return Result.Success("Company created succesfully");
            }
            catch (Exception ex)
            {
                return Result.Failure<string>(Error.Failure("Client.Companies.Create", $"Network error: {ex.Message}"));
            }
        }

        public async Task<Result<PagedResult<CompanyDTO>>> GetCompaniesAsync(int page, int pageSize, string? searchTerm = null)
        {
            try
            {
                var url = $"api/Company/get?page={page}&pageSize={pageSize}";

                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    url += $"&searchTerm={Uri.EscapeDataString(searchTerm)}";
                }

                var response = await _http.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return await response.ToFailureResultAsync<PagedResult<CompanyDTO>>("Client.Companies.Get");
                }

                var pagedResult = await response.Content.ReadFromJsonAsync<PagedResult<CompanyDTO>>();

                return pagedResult ?? new PagedResult<CompanyDTO>();
            }
            catch (Exception ex)
            {
                return Result.Failure<PagedResult<CompanyDTO>>(Error.Failure("Network", ex.Message));
            }
        }

        public async Task<Result<PagedResult<CompanyDTO>>> GetCompaniesByUserAsync(int page, int pageSize)
        {
            try
            {
                var url = $"api/Company/getByUser?page={page}&pageSize={pageSize}";

                var response = await _http.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return await response.ToFailureResultAsync<PagedResult<CompanyDTO>>("Client.Companies.GetByUser");
                }

                var pagedResult = await response.Content.ReadFromJsonAsync<PagedResult<CompanyDTO>>();

                return pagedResult ?? new PagedResult<CompanyDTO>();
            }
            catch (Exception ex)
            {
                return Result.Failure<PagedResult<CompanyDTO>>(Error.Failure("Network", ex.Message));
            }
        }

        public async Task<Result<SingleCompanyDTO>> GetCompanyByIdAsync(string companyId)
        {
            try
            {
                var queryString = new StringBuilder();
                queryString.Append("?CompanyId=").Append(companyId);

                var url = "/api/Company/getById" + queryString;

                var response = await _http.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return await response.ToFailureResultAsync<SingleCompanyDTO>("Client.Companies.GetById");
                }

                var result = await response.Content.ReadFromJsonAsync<SingleCompanyDTO>();

                return result;

            }catch(Exception ex)
            {
                return Result.Failure<SingleCompanyDTO>(Error.Failure("Client.Companies.GetById", $"Network error: {ex.Message}"));
            }
        }

        public async Task<Result<string>> UpdateCompanyAsync(Guid companyId, CreateCompanyInputModel model, IBrowserFile? image)
        {
            try
            {
                using var content = new MultipartFormDataContent();

                void AddString(string value, string name) =>
                    content.Add(new StringContent(value ?? string.Empty), name);

                AddString(companyId.ToString(), "CompanyId");

                AddString(model.Name, "Name");
                AddString(model.Description, "Description");
                AddString(model.Number, "Number");
                AddString(model.Street, "Street");
                AddString(model.City, "City");
                AddString(model.State, "State");

                if (image is not null)
                {
                    var maxFileSize = 5 * 1024 * 1024;
                    var fileContent = new StreamContent(image.OpenReadStream(maxFileSize));
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue(image.ContentType);
                    content.Add(fileContent, "Image", image.Name);
                }

                var response = await _http.PutAsync("api/Company/edit", content);

                if (!response.IsSuccessStatusCode)
                {
                    return await response.ToFailureResultAsync<string>("Client.Companies.Update");
                }

                return Result.Success("Company updated successfully");
            }
            catch (Exception ex)
            {
                return Result.Failure<string>(Error.Failure("Client.Companies.Update", $"Network error: {ex.Message}"));
            }
        }

        public async Task<Result> DeleteCompanyAsync(Guid companyId)
        {
            try
            {
                var url = $"api/Company/delete?companyId={companyId}";

                var response = await _http.DeleteAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return await response.ToFailureResultAsync<string>("Client.Companies.Delete");
                }

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(Error.Failure("Client.Companies.Delete", $"Network error: {ex.Message}"));
            }
        }
    }
}
