using MeetingSystem.Application.Abstractions.Data;
using MeetingSystem.Application.Abstractions.Messaging;
using MeetingSystem.Application.Abstractions.Services;
using MeetingSystem.Contracts.Users.Register;
using MeetingSystem.Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MeetingSystem.Application.Users.Register
{
    public class RegisterUserCommandHandler(IApplicationDbContext context,
        IPasswordHasher passwordHasher, ITokenService tokenService)
        : ICommandHandler<RegisterUserCommand, TokenResponse>
    {
        public async Task<Result<TokenResponse>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            if(await context.Users.AnyAsync(u => u.Email == command.Email, cancellationToken))
            {
                return Result.Failure<TokenResponse>(UserErrors.EmailNotUnique);
            }

            var user = new User(
                command.Email,
                command.FirstName,
                command.LastName,
                passwordHasher.HashPassword(command.Password));

            await context.Users.AddAsync(user, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);

            var token = tokenService.GenerateToken(user.Id);

            return new TokenResponse(token);
        }
    }
}
