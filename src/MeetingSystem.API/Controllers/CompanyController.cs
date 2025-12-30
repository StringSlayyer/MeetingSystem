using MeetingSystem.Application.Abstractions.Messaging;
using MeetingSystem.Application.Abstractions.Services;
using MeetingSystem.Application.Companies.CreateCompany;
using MeetingSystem.Application.Companies.GetCompanies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeetingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IDispatcher _dispatcher;

        public CompanyController(ITokenService tokenService, IDispatcher dispatcher)
        {
            _tokenService = tokenService;
            _dispatcher = dispatcher;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCompanyAsync([FromBody] CreateCompanyRequest request, CancellationToken cancellationToken)
        {
            var userId = _tokenService.GetUserIdFromClaimsPrincipal(User);
            if (userId == null || userId == Guid.Empty) return Unauthorized();
            var command = new CreateCompanyCommand(userId, request.Name, request.Description, request.Image, request.Number, request.Street, request.City, request.State);
            var result = await _dispatcher.Send(command, cancellationToken);
            return Ok(result);
        }


        [HttpGet("get")]
        public async Task<IActionResult> GetCompaniesAsync(CancellationToken cancellationToken)
        {
            var companies = await _dispatcher.Query(new GetCompaniesQuery(), cancellationToken);
            return Ok(companies);

        }

    }

    public sealed record CreateCompanyRequest(string Name, string Description,
        IFormFile? Image, string Number, string Street, string City, string State);
}
