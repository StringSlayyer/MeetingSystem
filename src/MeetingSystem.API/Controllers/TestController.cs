using MeetingSystem.Application.Abstractions.Messaging;
using MeetingSystem.Application.Test;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace MeetingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpPost("multiply")]
        public async Task<Result<int>> Multiply(int number, [FromServices] ICommandHandler<MultiplyByTwoCommand, int> handler, CancellationToken cancellationToken)
        {
            var command = new MultiplyByTwoCommand(number);
            Result<int> result = await handler.Handle(command, cancellationToken);
            return result;
        }
    }
}
