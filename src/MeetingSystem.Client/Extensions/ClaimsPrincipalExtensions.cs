using System.Security.Claims;

namespace MeetingSystem.Client.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal user)
        {
            var idClaim = user.FindFirst("UserId");
            if(idClaim != null && Guid.TryParse(idClaim.Value, out var guid))
            {
                return guid;
            }
            return Guid.Empty;
        }
    }
}
