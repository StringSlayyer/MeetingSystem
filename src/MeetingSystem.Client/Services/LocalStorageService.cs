using MeetingSystem.Client.Abstractions;
using Microsoft.JSInterop;
using System.Text.Json;

namespace MeetingSystem.Client.Services
{
    public class LocalStorageService(IJSRuntime jSRuntime) : ILocalStorageService
    {
        private readonly IJSRuntime _jsRuntime = jSRuntime;

        public async Task<T?> GetItemAsync<T>(string key)
        {
            var json = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", key);

            if (json == null)
                return default;
            return JsonSerializer.Deserialize<T>(json);
        }

        public async Task RemoveItemAsync(string key)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
        }

        public async Task SetItemAsync<T>(string key, T value)
        {
            var json = JsonSerializer.Serialize(value);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, json);
        }
    }
}
