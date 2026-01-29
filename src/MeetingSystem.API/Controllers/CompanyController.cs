using MeetingSystem.API.Extensions;
using MeetingSystem.Application.Abstractions.Messaging;
using MeetingSystem.Application.Abstractions.Services;
using MeetingSystem.Application.Companies.CreateCompany;
using MeetingSystem.Application.Companies.Delete;
using MeetingSystem.Application.Companies.GetById;
using MeetingSystem.Application.Companies.GetByUser;
using MeetingSystem.Application.Companies.GetCompanies;
using MeetingSystem.Application.Companies.Update;
using MeetingSystem.Contracts.Companies;
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
        public async Task<IActionResult> CreateCompanyAsync([FromForm] CreateCompanyRequest request, CancellationToken cancellationToken)
        {
            var userId = _tokenService.GetUserIdFromClaimsPrincipal(User);
            if (userId == null || userId == Guid.Empty) return Unauthorized();
            var command = new CreateCompanyCommand(userId, request.Name, request.Description, request.Image, request.Number, request.Street, request.City, request.State);
            var result = await _dispatcher.Send(command, cancellationToken);

            if(result.IsSuccess) return Ok(result);

            return BadRequest(result.Error);
        }


        [HttpGet("get")]
        public async Task<IActionResult> GetCompaniesAsync([FromQuery] GetCompaniesRequest request, CancellationToken cancellationToken)
        {
            var companies = await _dispatcher.Query(new GetCompaniesQuery(request.Page, request.PageSize, request.SearchTerm), cancellationToken);

            return companies.ToActionResult();

        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetCompanyByIdAsync([FromQuery] GetCompanyByIdRequest request, CancellationToken cancellationToken)
        {
            var company = await _dispatcher.Query(new GetCompanyByIdQuery(request.CompanyId), cancellationToken);

            return company.ToActionResult();
        }

        [HttpGet("getByUser")]
        public async Task<IActionResult> GetCompaniesByUserAsync([FromQuery] GetCompaniesByUserRequest request, CancellationToken cancellationToken)
        {
            var id = _tokenService.GetUserIdFromClaimsPrincipal(User);
            if (id == null || id == Guid.Empty) return Unauthorized();

            var companies = await _dispatcher.Query(new GetCompaniesByUserQuery(id, request.Page, request.PageSize), cancellationToken);

            return companies.ToActionResult();
        }

        [HttpPut("edit")]
        public async Task<IActionResult> UpdateCompanyAsync([FromForm] UpdateCompanyRequest request, CancellationToken cancellationToken)
        {
            var userId = _tokenService.GetUserIdFromClaimsPrincipal(User);
            if (userId == null || userId == Guid.Empty) return Unauthorized();

            var command = new UpdateCompanyCommand(
                request.CompanyId,
                userId,
                request.Name,
                request.Description,
                request.Image,
                request.Number,
                request.Street,
                request.City,
                request.State
            );

            var result = await _dispatcher.Send(command, cancellationToken);

            return result.ToActionResult();
        }

        [HttpDelete("{companyId}")]
        public async Task<IActionResult> DeleteCompanyAsync(Guid companyId, CancellationToken cancellationToken)
        {
            var userId = _tokenService.GetUserIdFromClaimsPrincipal(User);
            if (userId == null || userId == Guid.Empty) return Unauthorized();
            var command = new DeleteCompanyCommand(userId, companyId);
            var result = await _dispatcher.Send(command, cancellationToken);

            return result.ToActionResult();
        }
    }
    public sealed record CreateCompanyRequest(string Name, string Description,
        IFormFile? Image, string Number, string Street, string City, string State);
    public sealed record UpdateCompanyRequest(
            Guid CompanyId,
            string Name,
            string Description,
            IFormFile? Image,
            string Number,
            string Street,
            string City,
            string State);
}
