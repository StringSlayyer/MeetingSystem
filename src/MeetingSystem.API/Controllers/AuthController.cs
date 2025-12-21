using MeetingSystem.Application.Abstractions.Messaging;
using MeetingSystem.Application.Users.Register;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeetingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;
        public AuthController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command, CancellationToken cancellationToken)
        {
            var result = await _dispatcher.Send(command, cancellationToken);
            if(result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Error);
        }

    }
}
