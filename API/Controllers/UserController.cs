using API.Dtos;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        public UserController(UserManager<User> userManager, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _userManager = userManager;

        }
        [AllowAnonymous]
        [HttpGet("{email}")]
        public async Task<ActionResult<AuthorizedUserDto>> CheckIsVerified(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return NotFound("user not found");
            else
            {
                var token = _tokenService.GetToken(user.UserName);
                return Ok(new AuthorizedUserDto
                {
                    Email = user.Email,
                    UserName = user.UserName,
                    Token = token,
                    EmailConfirmed = user.EmailConfirmed
                });
            }
        }
    }
}