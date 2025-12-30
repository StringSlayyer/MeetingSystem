using SharedKernel;
using System.Net.Http.Json;

namespace MeetingSystem.Client.Extensions
{
    public static class HttpResponseExtension
    {
        public static async Task<Result<T>> ToFailureResultAsync<T>(this HttpResponseMessage response, string defaultCode)
        {
            try
            {
                var error = await response.Content.ReadFromJsonAsync<Error>();
                if (error != null) return Result.Failure<T>(error);
            }catch { }

            return Result.Failure<T>(Error.Failure(defaultCode, "API Request Failed"));
        }
    }
}
