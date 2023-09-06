using System.Security.Claims;

namespace API.Extensions
{
    public static class ClaimsExtensions
    {
        public static string GetUsernameFromToken(this ClaimsPrincipal claims)
        {
            return claims.FindFirst("Username").Value;
        }
    }
}