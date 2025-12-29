using MeetingSystem.Client.Abstractions;
using MeetingSystem.Contracts.Users;
using MeetingSystem.Contracts.Users.Register;
using SharedKernel;
using System.Net.Http.Json;
using static MeetingSystem.Contracts.Users.AuthRequests;

namespace MeetingSystem.Client.Services
{
    public class AuthService(HttpClient client) : IAuthService
    {
        private readonly HttpClient _http = client;
        
        public async Task<Result<TokenResponse>> LoginAsync(LoginUserRequest request)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("api/Auth/login", request);

                if (!response.IsSuccessStatusCode)
                {
                    return Result.Failure<TokenResponse>(Error.Failure("Client.Login","Invalid credentials or server error"));
                }

                var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>();

                if (tokenResponse == null)
                {
                    return Result.Failure<TokenResponse>(Error.Failure("Client.Login", "Server returned empty response"));
                }


                return Result.Success<TokenResponse>(tokenResponse);
            }catch(Exception ex)
            {
                return Result.Failure<TokenResponse>(Error.Failure("Client.Login", $"Network error: {ex.Message}"));
            }
        }

        public Task<Result<TokenResponse>> RegisterAsync(RegisterUserRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
