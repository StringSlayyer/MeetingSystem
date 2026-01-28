using MeetingSystem.Contracts.Reservations;
using SharedKernel;

namespace MeetingSystem.Client.Abstractions
{
    public interface IReservationService
    {
        Task<Result<List<ReservationDTO>>> GetReservationsByResourceAsync(GetReservationByResourceRequest request);
        Task<Result<Guid>> CreateReservationAsync(CreateReservationRequest request);
        Task<Result> CancelReservationAsync(Guid reservationId);
    }
}
