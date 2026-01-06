using MeetingSystem.Client.Abstractions;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net;
using System.Net.Http.Headers;

namespace MeetingSystem.Client.Auth
{
    public class JwtInterceptor(ILocalStorageService localStorage, AuthenticationStateProvider stateProvider) : DelegatingHandler
    {
        private readonly ILocalStorageService _localStorage = localStorage;
        private readonly CustomAuthStateProvider _authProvider = (CustomAuthStateProvider)stateProvider;
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");

            if (!string.IsNullOrEmpty(token)  && request.Headers.Authorization == null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            var response = await base.SendAsync(request, cancellationToken);

            if(response.StatusCode == HttpStatusCode.Unauthorized)
            {
                await _authProvider.MarkUserAsLoggedOut();
            }

            return response;
        }
    }
}
