using MeetingSystem.Client.Abstractions;
using System.Net.Http.Headers;

namespace MeetingSystem.Client.Auth
{
    public class JwtInterceptor(ILocalStorageService localStorage) : DelegatingHandler
    {
        private readonly ILocalStorageService _localStorage = localStorage;
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
