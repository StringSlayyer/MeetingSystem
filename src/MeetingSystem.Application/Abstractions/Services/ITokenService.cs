using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace MeetingSystem.Application.Abstractions.Services
{
    public interface ITokenService
    {
        string GenerateToken(Guid userId);
        TokenValidationParameters GetTokenValidationParameters();
        Guid GetUserIdFromClaimsPrincipal(ClaimsPrincipal user);
    }
}
