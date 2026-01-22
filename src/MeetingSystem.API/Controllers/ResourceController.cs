using MeetingSystem.Application.Abstractions.Messaging;
using MeetingSystem.Application.Abstractions.Services;
using MeetingSystem.Application.Resources.AddMeetingRoom;
using MeetingSystem.Application.Resources.AddParkingSpot;
using MeetingSystem.Application.Resources.GetByCompany;
using MeetingSystem.Application.Resources.GetById;
using MeetingSystem.Contracts.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeetingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;
        private readonly ITokenService _tokenService;

        public ResourceController(IDispatcher dispatcher, ITokenService tokenService)
        {
            _dispatcher = dispatcher;
            _tokenService = tokenService;
        }

        [HttpPost("addMeetingRoom")]
        public async Task<IActionResult> AddMeetingRoomAsync([FromForm] AddMeetingRoomRequest request, CancellationToken cancellationToken)
        {
            var userId = _tokenService.GetUserIdFromClaimsPrincipal(User);
            if(userId == Guid.Empty) return Unauthorized();
            var command = new AddMeetingRoomCommand(userId, request.Name, request.CompanyId, request.Description, request.PricePerHour, request.Image, request.Capacity, request.Features);
            var result = await _dispatcher.Send(command, cancellationToken);

            if (result.IsSuccess) return Ok(result);

            return BadRequest(result.Error);
        }

        [HttpGet("getByCompany")]
        public async Task<IActionResult> GetResourcesByCompanyAsync([FromQuery] GetResourcesByCompanyRequest request, CancellationToken cancellationToken)
        {
            var query = new GetResourcesByCompanyQuery(request.CompanyId);
            var result = await _dispatcher.Query(query, cancellationToken);
            if (result.IsSuccess) return Ok(result);

            return BadRequest(result.Error);
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetResourceByIdAsync([FromQuery] GetResourceByIdRequest request, CancellationToken cancellationToken)
        {
            var query = new GetResourceByIdQuery(request.Id);
            var result = await _dispatcher.Query(query, cancellationToken);
            if (result.IsSuccess) return Ok(result);

            return BadRequest(result.Error);
        }

        [HttpPost("addParkingSpot")]
        public async Task<IActionResult> AddParkingSpotAsync([FromForm] AddParkingSpotRequest request, CancellationToken cancellationToken)
        {
            var userId = _tokenService.GetUserIdFromClaimsPrincipal(User);
            if (userId == Guid.Empty) return Unauthorized();
            var command = new AddParkingSpotCommand(userId, request.Name, request.CompanyId, request.Description, request.PricePerHour, request.Image, request.Capacity, request.IsCovered);
            var result = await _dispatcher.Send(command, cancellationToken);

            if (result.IsSuccess) return Ok(result);

            return BadRequest(result.Error);
        }
    }

    public sealed record AddMeetingRoomRequest(string Name, Guid CompanyId,
        string Description, decimal PricePerHour, IFormFile? Image, int Capacity, List<string> Features);
    public sealed record AddParkingSpotRequest(string Name, Guid CompanyId,
       string Description, decimal PricePerHour, IFormFile? Image, int Capacity, bool IsCovered);

}
