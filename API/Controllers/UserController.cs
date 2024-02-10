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
    /// <summary>
    /// Manages user-related operations such as user verification, retrieval, editing, and search.
    /// </summary>
    public class UserController : BaseApiController
    {
        /// <summary>
        /// Manages user-related operations such as finding correct user.
        /// </summary>
        private readonly UserManager<User> _userManager;
        /// <summary>
        /// Service for generating tokens.
        /// </summary>
        private readonly ITokenService _tokenService;
        /// <summary>
        /// Maps between different object types, facilitating data transformation.
        /// </summary>
        private readonly IMapper _mapper;
        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="tokenService"></param>
        /// <param name="mapper"></param>
        public UserController(UserManager<User> userManager, ITokenService tokenService, IMapper mapper)
        {
            _mapper = mapper;
            _tokenService = tokenService;
            _userManager = userManager;

        }
        /// <summary>
        /// Checks if a user with the specified email is verified.
        /// </summary>
        /// <param name="email">email of user that we want to verify</param>
        /// <returns>Status code of operation with authorized user credentials</returns>
        /// <response code="200">If email has been verified sucessfuly</response>
        /// <response code="404">If user has been not found</response>
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
        /// <summary>
        /// Retrieves a user by their ID.
        /// </summary>
        /// <param name="id">id of user that we want to retrieve</param>
        /// <returns>Status code of operation with retrieved user object</returns>
        /// <response code="200">If user has been retrieved sucessfuly</response>
        /// <response code="404">If user has been not found</response>
        [HttpGet("id/{id}")]
        public async Task<ActionResult<UserClientDto>> GetUserById(string id)
        {
            var user = await _userManager.Users.Include(u => u.Followed).Include(u => u.Followers).FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) return NotFound();
            return Ok(_mapper.Map<UserClientDto>(user));
        }
        /// <summary>
        ///  Edits user information based on the provided DTO.
        /// </summary>
        /// <param name="editUserDto">data transfer object that contains edited properties of user</param>
        /// <returns>Status code of operation with edited user object</returns>
        /// <response code="200">If user has been edited sucessfuly</response>
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
            return Ok(_mapper.Map<UserClientDto>(user));
        }
        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>Status code of operation with list of all users</returns>
        /// <response code="200">If users have been retrieved sucessfuly</response>
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<UserClientDto>>> GetAllUsers()
        {
            var users = await _userManager.Users.Include(u => u.Followed).Include(u => u.Followers).ToListAsync();
            return Ok(_mapper.Map<IEnumerable<UserClientDto>>(users));
        }
        /// <summary>
        /// Searches for users based on the provided search string.
        /// </summary>
        /// <param name="searchstring">string that contains searched name, last name or username of user</param>
        /// <returns>Status code of operation with list of users that was found</returns>
        /// <response code="200">If user have been found sucessfuly</response>
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