using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Helpers
{
    /// <summary>
    /// Service for generating JWT tokens.
    /// </summary>
    public class TokenService: ITokenService
    {
        /// <summary>
        /// Configuration instance for retrieving JWT key.
        /// </summary>
        private readonly IConfiguration _configuration;
        /// <summary>
        /// Initializes a new instance of the <see cref="TokenService"/> class with the specified configuration.
        /// </summary>
        /// <param name="configuration">The configuration instance.</param>
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;

        }
        /// <summary>
        /// Generates a JWT token for the specified username.
        /// </summary>
        /// <param name="username">The username for which the token is generated.</param>
        /// <returns>The generated JWT token.</returns>
        public string GetToken(string username)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["jwtKey"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Username", username),

                 }),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}