using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly UserManager<User> _userManager;
        public UserController(UserManager<User> userManager)
        {
            _userManager = userManager;

        }
        [HttpGet("{email}")]
        public async Task<ActionResult<User>> GetUserByUsername(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if(user == null) return NotFound("user not found");
            else return Ok(user);
        }
    }
}