using System.Text.RegularExpressions;
using API.Dtos;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [AllowAnonymous]
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

                HandleEmail(newUser,false);

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
        [HttpPost("forgetpassword")]
        public async Task<ActionResult> ResetPassword([FromQuery] string userId, [FromQuery] string code, [FromQuery] string newPassword)
        {
            if (userId == null || code == null) return BadRequest("Invalid email confirmation url");
             var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return BadRequest("Invalid email parameter");
            var result = await _userManager.ResetPasswordAsync(user,code,newPassword);
            var status = result.Succeeded ? "Password set successful" : "Try again later, password change went wrong";
            return Ok(status);
        }

        [HttpGet("confirmemail")]
        public async Task<ActionResult> ConfirmEmail([FromQuery] string userId, [FromQuery] string code)
        {
            if (userId == null || code == null) return BadRequest("Invalid email confirmation url");
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return BadRequest("Invalid email parameter");
            var token = _tokenService.GetToken(user.UserName);
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if(result.Succeeded) return Ok(token);
            return BadRequest("Please try again later");
           
        }


        [HttpPost("sendEmail")]
        public async Task<ActionResult> GetNewVerifyEmail([FromQuery] string email, [FromQuery] bool isPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return NotFound("Email not found");
            if (user.EmailConfirmed && !isPassword) return Ok("Email is Already verified");
            HandleEmail(user, isPassword);
            return Ok("Email has been sent, check your inbox. If you don't see verification Email, check your spam");
        }
        private async void HandleEmail(User newUser, bool isPassword)
        {
            var code = "";
            var email_body = GetHtmlBody.GetBody();
            var callback = "";
            if (isPassword)
            {
                code = await _userManager.GeneratePasswordResetTokenAsync(newUser);
                callback = Request.Scheme + "://" + Request.Host + Url.Action("forgetpassword", "Auth", new { userId = newUser.Id, code = code });
            }
            else
            {
                code = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                callback = Request.Scheme + "://" + Request.Host + Url.Action("confirmEmail", "Auth", new { userId = newUser.Id, code = code });
            }
           
            var body = email_body.Replace("#URL#", System.Text.Encodings.Web.HtmlEncoder.Default.Encode(callback));
            _emailSender.SendEmail(body, newUser.Email);
        }
        private bool IsEmailValid(string email)
        {

            string pattern = @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$";


            return Regex.IsMatch(email, pattern);
        }
    }
}