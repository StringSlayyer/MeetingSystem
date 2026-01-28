using MeetingSystem.Client.Abstractions;
using MeetingSystem.Client.Extensions;
using MeetingSystem.Contracts.Reservations;
using SharedKernel;
using System.Net.Http.Json;
using System.Text;

namespace MeetingSystem.Client.Services
{
    public class ReservationService(HttpClient client) : IReservationService
    {
        private readonly HttpClient _http = client;

        public async Task<Result> CancelReservationAsync(Guid reservationId)
        {
            try
            {
                var response = await _http.DeleteAsync($"api/Reservation/{reservationId}/cancel");

                if (!response.IsSuccessStatusCode)
                {
                    return await response.ToFailureResultAsync<string>("Reservation.Cancel");
                }

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(Error.Failure("Client.Reservation", ex.Message));
            }
        }

        public async Task<Result<Guid>> CreateReservationAsync(CreateReservationRequest request)
        {
            try
            { 
                var response = await _http.PostAsJsonAsync("/api/Reservation/create", request);

                if (!response.IsSuccessStatusCode)
                {
                    return await response.ToFailureResultAsync<Guid>("Client.Reservation.Create");
                }

                var result = await response.Content.ReadFromJsonAsync<Result<Guid>>();

                return result;
            }
            catch(Exception ex)
            {
                return Result.Failure<Guid>(Error.Failure("Client.Reservation.Create", $"Network error: {ex.Message}"));
            }
        }

        public async Task<Result<List<ReservationDTO>>> GetReservationsByResourceAsync(GetReservationByResourceRequest request)
        {
            try
            {
                var queryString = new StringBuilder();
                queryString.Append("?ResourceId=").Append(request.ResourceId);
                queryString.Append("&Start=").Append(request.Start.Value.ToString("s"));
                queryString.Append("&End=").Append(request.End.Value.ToString("s"));

                var url = "api/Reservation/getByResource" + queryString;
                var response = await _http.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return await response.ToFailureResultAsync<List<ReservationDTO>>("Client.Reservation.GetByResource");
                }

                var result = await response.Content.ReadFromJsonAsync<Result<List<ReservationDTO>>>();

                return result;
            }
            catch(Exception ex)
            {
                return Result.Failure<List<ReservationDTO>>(Error.Failure("Client.Reservation.GetByResource", $"Network error: {ex.Message}"));
            }
        }
    }
}
