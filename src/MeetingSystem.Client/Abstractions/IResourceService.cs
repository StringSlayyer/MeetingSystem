using MeetingSystem.Client.Models;
using MeetingSystem.Contracts.Resources;
using Microsoft.AspNetCore.Components.Forms;
using SharedKernel;

namespace MeetingSystem.Client.Abstractions
{
    public interface IResourceService
    {
        Task<Result<Guid>> AddResourceAsync(AddResourceInputModel model, IBrowserFile? image);
        Task<Result<ResourceDTO>> GetResourceByIdAsync(GetResourceByIdRequest request);
        Task<Result<string>> UpdateResourceAsync(Guid resourceId, EditResourceInputModel model, IBrowserFile? image);
        Task<Result> DeleteResourceAsync(Guid resourceId);
    }
}
