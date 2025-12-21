using MeetingSystem.Application.Abstractions.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MeetingSystem.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(Guid userId)
        {
            var claims = new List<Claim>
            {
                new Claim("UserId", userId.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT_SECRET_KEY"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT_ISSUER"],
                audience: _configuration["JWT_AUDIENCE"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        public TokenValidationParameters GetTokenValidationParameters()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT_SECRET_KEY"]));
            if (key == null)
            {
                throw new Exception("key is not configured.");
            }
            return new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _configuration["JWT_ISSUER"],
                ValidAudience = _configuration["JWT_AUDIENCE"],
                IssuerSigningKey = key,
                ClockSkew = TimeSpan.Zero
            };

        }

        public Guid GetUserIdFromClaimsPrincipal(ClaimsPrincipal user)
        {
            var userIdClaims = user?.FindFirst("UserId")?.Value;
            return userIdClaims != null ? Guid.Parse(userIdClaims) : Guid.Empty;
        }
    }
}
