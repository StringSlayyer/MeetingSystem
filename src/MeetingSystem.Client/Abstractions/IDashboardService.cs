using MeetingSystem.Contracts.Dashboard;
using SharedKernel;

namespace MeetingSystem.Client.Abstractions
{
    public interface IDashboardService
    {
        Task<Result<UserDashboardDTO>> GetUserDashboardAsync();
    }
}
