namespace API.Interfaces
{
    /// <summary>
    /// Interface for generating JWT tokens.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Generates a JWT token for the specified username.
        /// </summary>
        /// <param name="username">The username for which the token is generated.</param>
        /// <returns>The generated JWT token.</returns>
        string GetToken(string username);
    }
}