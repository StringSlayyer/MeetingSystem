using MeetingSystem.Application.Abstractions.Messaging;
using MeetingSystem.Application.Abstractions.Services;
using MeetingSystem.Application.Reservations.Cancel;
using MeetingSystem.Application.Reservations.Create;
using MeetingSystem.Application.Reservations.GetByResource;
using MeetingSystem.Contracts.Reservations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeetingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;
        private readonly ITokenService _tokenService;

        public ReservationController(IDispatcher dispatcher, ITokenService tokenService)
        {
            _dispatcher = dispatcher;
            _tokenService = tokenService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateReservationAsync([FromBody] CreateReservationRequest request, CancellationToken cancellationToken)
        {
            var userId = _tokenService.GetUserIdFromClaimsPrincipal(User);
            if (userId == Guid.Empty) return Unauthorized();
            var command = new CreateReservationCommand(request.ResourceId, userId, request.StartTime, request.EndTime, request.Note, request.AttendeeEmails);
            var result = await _dispatcher.Send(command, cancellationToken);

            if (result.IsSuccess) return Ok(result);

            return BadRequest(result.Error);
        }

        [HttpGet("getByResource")]
        public async Task<IActionResult> GetReservationByResourceAsync([FromQuery] GetReservationByResourceRequest request, CancellationToken cancellationToken)
        {
            var query = new GetReservationsByResourceQuery(request.ResourceId, request.Start, request.End);
            var result = await _dispatcher.Query(query, cancellationToken);

            if (result.IsSuccess) return Ok(result);

            return BadRequest(result.Error);
        }

        [HttpDelete("{reservationId}/cancel")]
        public async Task<IActionResult> CancelReservationAsync(Guid reservationId, CancellationToken cancellationToken)
        {
            var userId = _tokenService.GetUserIdFromClaimsPrincipal(User);
            if (userId == null || userId == Guid.Empty) return Unauthorized();

            var command = new CancelReservationCommand(userId, reservationId);

            var result = await _dispatcher.Send(command, cancellationToken);

            if (result.IsSuccess) return Ok();

            return BadRequest(result.Error);
        }
    }

  

}
