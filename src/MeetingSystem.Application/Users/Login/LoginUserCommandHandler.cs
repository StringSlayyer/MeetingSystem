using MeetingSystem.Application.Abstractions.Data;
using MeetingSystem.Application.Abstractions.Messaging;
using MeetingSystem.Application.Abstractions.Services;
using MeetingSystem.Contracts.Users.Register;
using MeetingSystem.Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Application.Users.Login
{
    public sealed class LoginUserCommandHandler(IApplicationDbContext context,
        IPasswordHasher passwordHasher, ITokenService tokenService)
        : ICommandHandler<LoginUserCommand, TokenResponse>
    {
        public async Task<Result<TokenResponse>> Handle(LoginUserCommand command, CancellationToken cancellationToken)
        {
            User? user = await context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.Email == command.Email, cancellationToken);

            if (user is null)
            {
                return Result.Failure<TokenResponse>(UserErrors.NotFoundByEmail);
            }

            bool passwordValid = passwordHasher.VerifyPassword(command.Password, user.PasswordHash);

            if (!passwordValid)
            {
                return Result.Failure<TokenResponse>(UserErrors.NotFoundByEmail);
            }

            string token = tokenService.GenerateToken(user.Id);

            return new TokenResponse(token);
        }
    }
}
