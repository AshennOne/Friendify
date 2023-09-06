using System.Text.RegularExpressions;
using API.Dtos;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AuthController : BaseApiController
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IEmailSender _emailSender;
        public AuthController(UserManager<User> userManager, ITokenService tokenService, IEmailSender emailSender)
        {
            _emailSender = emailSender;
            _tokenService = tokenService;
            _userManager = userManager;
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<AuthorizedUserDto>> Login([FromBody] LoginDto loginDto)
        {
            User user;
            if (IsEmailValid(loginDto.UserNameOrEmail))
            {
                user = await _userManager.FindByEmailAsync(loginDto.UserNameOrEmail);
            }
            else
            {
                user = await _userManager.FindByNameAsync(loginDto.UserNameOrEmail);
            }
            if (user == null) return NotFound("User not found");
            if (!user.EmailConfirmed)
            {
                return BadRequest("Email needs to be confirmed");
            }
            if (await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                var token = _tokenService.GetToken(user.UserName);
                return Ok(new AuthorizedUserDto
                {
                    Username = user.UserName,
                    Email = user.Email,
                    Token = token
                });
            }
            else
            {
                return Unauthorized();
            }

        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<AuthorizedUserDto>> Register([FromBody] RegisterDto registerDto)
        {
            var user = await _userManager.FindByEmailAsync(registerDto.Email);
            if (user != null) return BadRequest("Email already exist");
            user = await _userManager.FindByNameAsync(registerDto.UserName);
            if (user != null) return BadRequest("Username already exist");
            var newUser = new User
            {
                UserName = registerDto.UserName,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                EmailConfirmed = false
            };
            var result = await _userManager.CreateAsync(newUser, registerDto.Password);
            if (!result.Succeeded)
            {
                return BadRequest("Something went wrong");
            }
            else
            {

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                var email_body = $"Please confirm your email address <a href=\"URL\">Click here </a>";
                var callback = Request.Scheme + "://" + Request.Host + Url.Action("ConfirmEmail", "Auth", new { userId = newUser.Id, code = code });
                var body = email_body.Replace("URL", System.Text.Encodings.Web.HtmlEncoder.Default.Encode(callback));
                _emailSender.SendEmail(body, newUser.Email);

                return Ok("Succesfully registered, please verify your email");

                // var token = _tokenService.GetToken(registerDto.UserName);
                // return Ok(new AuthorizedUserDto
                // {
                //     Username = registerDto.UserName,
                //     Email = registerDto.Email,
                //     Token = token
                // });
            }
        }
        [AllowAnonymous]
        [HttpGet("confirmemail")]
        public async Task<ActionResult> ConfirmEmail([FromQuery]string userId, [FromQuery]string code)
        {
            if (userId == null || code == null) return BadRequest("Invalid email confirmation url");
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return BadRequest("Invalid email parameter");
            
            var result = await _userManager.ConfirmEmailAsync(user, code);
            var status = result.Succeeded ? "Thank you for confirming your mail" : "Your email is not confirmed, please try again later";
            return Ok(status);
        }
        private bool IsEmailValid(string email)
        {

            string pattern = @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$";


            return Regex.IsMatch(email, pattern);
        }


    }
}