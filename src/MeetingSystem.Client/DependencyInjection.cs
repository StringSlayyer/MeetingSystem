using MeetingSystem.Client.Abstractions;
using MeetingSystem.Client.Auth;
using MeetingSystem.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen;

namespace MeetingSystem.Client
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddAuthorizationCore();
            services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
            services.AddTransient<JwtInterceptor>();
            services.AddScoped<ILocalStorageService, LocalStorageService>();
            services.AddHttpClient("API", client =>
                client.BaseAddress = new Uri("http://localhost:8080"))
                .AddHttpMessageHandler<JwtInterceptor>();
            services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("API"));
            services.AddRadzenComponents();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<ICompanyService, CompanyService>();

            return services;
        }
    }
}
