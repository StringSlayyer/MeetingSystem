using MeetingSystem.API.Extensions;
using MeetingSystem.Application.Abstractions.Messaging;
using MeetingSystem.Application.Abstractions.Services;
using MeetingSystem.Application.Dashboards.Get;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeetingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;
        private readonly ITokenService _tokenService;

        public DashboardController(IDispatcher dispatcher, ITokenService tokenService)
        {
            _dispatcher = dispatcher;
            _tokenService = tokenService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetDashboardForUserAsync(CancellationToken cancellationToken)
        {
            var userId = _tokenService.GetUserIdFromClaimsPrincipal(User);
            if (userId == Guid.Empty) return Unauthorized();
            var query = new GetUserDashboardQuery(userId);
            var result = await _dispatcher.Query(query, cancellationToken);

            return result.ToActionResult();
        }
    }
}
