using MeetingSystem.Client.Abstractions;
using MeetingSystem.Client.Extensions;
using MeetingSystem.Contracts.Dashboard;
using SharedKernel;
using System.Net.Http.Json;

namespace MeetingSystem.Client.Services
{
    public class DashboardService(HttpClient client) : IDashboardService
    {
        private readonly HttpClient _http = client;
        public async Task<Result<UserDashboardDTO>> GetUserDashboardAsync()
        {
            try
            {
                var response = await _http.GetAsync("api/Dashboard");

                if (!response.IsSuccessStatusCode)
                {
                    return await response.ToFailureResultAsync<UserDashboardDTO>("Client.Dashboard");
                }

                var dashboardResponse = await response.Content.ReadFromJsonAsync<Result<UserDashboardDTO>>();

                if (dashboardResponse == null)
                {
                    return Result.Failure<UserDashboardDTO>(Error.Failure("Client.Dashboard", "Server returned empty response"));
                }

                return dashboardResponse;
            }catch(Exception ex)
            {
                return Result.Failure<UserDashboardDTO>(Error.Failure("Client.Dashboard", $"Network error: {ex.Message}"));
            }
        }
    }
}
