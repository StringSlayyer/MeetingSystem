using MeetingSystem.Client.Abstractions;
using MeetingSystem.Client.Extensions;
using MeetingSystem.Client.Models;
using MeetingSystem.Contracts.Resources;
using Microsoft.AspNetCore.Components.Forms;
using SharedKernel;
using System.Globalization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace MeetingSystem.Client.Services
{
    public class ResourceService(HttpClient client) : IResourceService
    {
        private readonly HttpClient _http = client;

        public async Task<Result<Guid>> AddResourceAsync(AddResourceInputModel model, IBrowserFile? image)
        {
            try
            {
                using var content = new MultipartFormDataContent();

                void AddString(string value, string name) =>
                    content.Add(new StringContent(value ?? string.Empty), name);

                AddString(model.Name, "Name");
                AddString(model.CompanyId.ToString(), "CompanyId");
                AddString(model.Description, "Description");
                AddString(model.PricePerHour.ToString(CultureInfo.InvariantCulture), "PricePerHour");

                string endpoint = "";

                if(model.Type == ResourceType.MeetingRoom)
                {
                    endpoint = "api/Resource/addMeetingRoom";

                    AddString(model.Capacity.ToString(), "Capacity");

                    for(int i = 0; i < model.Features.Count; i++)
                    {
                        AddString(model.Features[i], $"Features[{i}]");
                    }
                }


                if (image != null)
                {
                    var imageContent = new StreamContent(image.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024)); 
                    imageContent.Headers.ContentType = new MediaTypeHeaderValue(image.ContentType);
                    content.Add(imageContent, "Image", image.Name);
                }

                var response = await _http.PostAsync(endpoint, content);

                if (!response.IsSuccessStatusCode)
                {
                    return await response.ToFailureResultAsync<Guid>("Client.Resources.AddResource");
                }

                var result = await response.Content.ReadFromJsonAsync<Guid>();
                return result;

            }catch(Exception ex)
            {
                return Result.Failure<Guid>(Error.Failure("Network", ex.Message));
            }
        }

        public async Task<Result<ResourceDTO>> GetResourceByIdAsync(GetResourceByIdRequest request)
        {
            try
            {
                var queryString = new StringBuilder();
                queryString.Append("?Id=").Append(request.Id);

                var url = "/api/Resource/getById" + queryString;

                var response = await _http.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return await response.ToFailureResultAsync<ResourceDTO>("Client.Resource.GetById");
                }

                var result = await response.Content.ReadFromJsonAsync<Result<ResourceDTO>>();

                return result;
            }
            catch(Exception ex)
            {
                return Result.Failure<ResourceDTO>(Error.Failure("Client.Resource.GetById", $"Network error: {ex.Message}"));
            }
            throw new NotImplementedException();
        }
    }
}
