using MeetingSystem.Client.Abstractions;
using MeetingSystem.Client.Extensions;
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
                    return await response.ToFailureResultAsync<TokenResponse>("Client.Login");
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

        public async Task<Result<TokenResponse>> RegisterAsync(RegisterUserRequest request)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("api/Auth/register", request);

                if (!response.IsSuccessStatusCode)
                {
                    return await response.ToFailureResultAsync<TokenResponse>("Clien.Register");
                }

                var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>();

                if(tokenResponse == null)
                {
                    return Result.Failure<TokenResponse>(Error.Failure("Client.Register", "Server returned empty response"));
                }

                return tokenResponse;
            }
            catch(Exception ex)
            {
                return Result.Failure<TokenResponse>(Error.Failure("Client.Register", $"Network error: {ex.Message}"));
            }
        }

    }
}
