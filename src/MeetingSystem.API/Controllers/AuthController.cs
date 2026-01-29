using MeetingSystem.API.Extensions;
using MeetingSystem.Application.Abstractions.Messaging;
using MeetingSystem.Application.Users.Login;
using MeetingSystem.Application.Users.Register;
using MeetingSystem.Contracts.Users;
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
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request, CancellationToken cancellationToken)
        {
            var command = new RegisterUserCommand(
                request.Email,
                request.FirstName,
                request.LastName,
                request.Password);

            var result = await _dispatcher.Send(command, cancellationToken);

            return result.ToActionResult();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest request, CancellationToken cancellationToken)
        {
            var command = new LoginUserCommand(
                request.Email,
                request.Password);

            var result = await _dispatcher.Send(command, cancellationToken);
            
            return result.ToActionResult();
        }

    }

}
