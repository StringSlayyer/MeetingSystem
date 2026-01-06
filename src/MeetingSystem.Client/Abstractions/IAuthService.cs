using MeetingSystem.Contracts.Users.Register;
using SharedKernel;
using MeetingSystem.Contracts.Users;

namespace MeetingSystem.Client.Abstractions
{
    public interface IAuthService
    {
        Task<Result<TokenResponse>> LoginAsync(LoginUserRequest request);
        Task<Result<TokenResponse>> RegisterAsync(RegisterUserRequest request);
    }
}
