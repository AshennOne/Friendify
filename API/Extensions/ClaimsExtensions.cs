using System.Security.Claims;

namespace API.Extensions
{
    /// <summary>
    /// Provides extension methods for extracting information from claims in ClaimsPrincipal objects.
    /// </summary>
    public static class ClaimsExtensions
    {
        /// <summary>
        /// Extension method for retrieving the username from the claims in the provided ClaimsPrincipal.
        /// </summary>
        /// <param name="claims"></param>
        /// <returns>The username extracted from the claims</returns>
        public static string GetUsernameFromToken(this ClaimsPrincipal claims)
        {
            return claims.FindFirst("Username").Value;
        }
    }
}