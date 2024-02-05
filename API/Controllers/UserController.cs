using API.Dtos;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        public UserController(UserManager<User> userManager, ITokenService tokenService, IMapper mapper)
        {
            _mapper = mapper;
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
                    EmailConfirmed = user.EmailConfirmed,
                    ImgUrl = user.ImgUrl
                });
            }
        }
        [HttpGet("id/{id}")]
        public async Task<ActionResult<UserClientDto>> GetUserById(string id)
        {
            var user = await _userManager.Users.Include(u => u.Followed).Include(u => u.Followers).FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) return NotFound();
            return Ok(_mapper.Map<UserClientDto>(user));
        }
        [HttpPut]
        public async Task<ActionResult<UserClientDto>> EditUser([FromBody] EditUserDto editUserDto)
        {
            var username = User.GetUsernameFromToken();
            var user = await _userManager.FindByNameAsync(username);
            if (editUserDto.Bio != null && editUserDto.Bio.Length > 1)
            {
                user.Bio = editUserDto.Bio;
            }
            if (editUserDto.ImgUrl != null && editUserDto.ImgUrl.Length > 1)
            {
                user.ImgUrl = editUserDto.ImgUrl;
            }
            await _userManager.UpdateAsync(user);
            return _mapper.Map<UserClientDto>(user);
        }
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<UserClientDto>>> GetAllUsers()
        {
            var users = await _userManager.Users.Include(u => u.Followed).Include(u => u.Followers).ToListAsync();
            return Ok(_mapper.Map<IEnumerable<UserClientDto>>(users));
        }
        [HttpGet("search/{searchstring}")]
        public async Task<ActionResult<IEnumerable<UserClientDto>>> SearchForUsers(string searchstring)
        {
            if (searchstring.Contains(" "))
            {
                var pieces = searchstring.Split(" ");
                var users = await _userManager.Users.Where(u => u.FirstName.ToLower().Contains(pieces[0].ToLower()) || u.LastName.ToLower().Contains(pieces[1].ToLower()) || u.UserName.ToLower().Contains(pieces[0].ToLower()) || u.UserName.ToLower().Contains(pieces[1].ToLower())).ToListAsync();
                return Ok(_mapper.Map<IEnumerable<UserClientDto>>(users));
            }
            else
            {
                var users = await _userManager.Users.Where(u => u.FirstName.ToLower().Contains(searchstring.ToLower()) || u.LastName.ToLower().Contains(searchstring.ToLower()) || u.UserName.ToLower().Contains(searchstring.ToLower())).ToListAsync();
                return Ok(_mapper.Map<IEnumerable<UserClientDto>>(users));
            }

        }
    }
}