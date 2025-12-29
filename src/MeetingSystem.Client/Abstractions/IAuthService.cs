using MeetingSystem.Contracts.Users.Register;
using SharedKernel;
using static MeetingSystem.Contracts.Users.AuthRequests;

namespace MeetingSystem.Client.Abstractions
{
    public interface IAuthService
    {
        Task<Result<TokenResponse>> LoginAsync(LoginUserRequest request);
        Task<Result<TokenResponse>> RegisterAsync(RegisterUserRequest request);
    }
}
