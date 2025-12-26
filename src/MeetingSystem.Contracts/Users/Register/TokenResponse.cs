using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingSystem.Contracts.Users.Register
{
    public sealed class TokenResponse
    {
        public string Token { get; }
        public TokenResponse(string token)
        {
            Token = token;
        }
    }
}
