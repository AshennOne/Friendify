using System.Text.RegularExpressions;
using API.Dtos;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    /// <summary>
    /// This class is responsible for whole authentication logic - registration process, log in, reset password and email verification
    /// </summary>
    [AllowAnonymous]
    public class AuthController : BaseApiController
    {
        /// <summary>
        /// Manages user-related operations such as registration, login, and generating tokens
        /// </summary>
        private readonly UserManager<User> _userManager;
        /// <summary>
        /// Generates and manages jwt authentication tokens for user.
        /// </summary>
        private readonly ITokenService _tokenService;
        /// <summary>
        /// Sends email to user for account-related actions.
        /// </summary>
        private readonly IEmailSender _emailSender;
        /// <summary>
        /// Maps between different object types, facilitating data transformation.
        /// </summary>
        private readonly IMapper _mapper;
        /// <summary>
        /// Provides access to the unit of work for interacting with the database.
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="tokenService"></param>
        /// <param name="emailSender"></param>
        /// <param name="mapper"></param>
        /// <param name="unitOfWork"></param>
        public AuthController(UserManager<User> userManager, ITokenService tokenService, IEmailSender emailSender, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailSender = emailSender;
            _tokenService = tokenService;
            _userManager = userManager;
        }
        /// <summary>
        ///Handles user login by verifying credentials and generating an authentication token.
        /// </summary>
        /// <param name="loginDto">instance of data transfer object that contains log in credentials</param>
        /// <returns>Status code of operation with authorized user credentials</returns>
        /// <response code="200">Returns authorized user credentials</response>
        /// <response code="404">If user doesn't exists</response>
        /// <response code="400">If user exists, but credentials are invalid</response>
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
            if (await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                if (!user.EmailConfirmed)
                {
                    return BadRequest("Email needs to be confirmed");
                }
                var token = _tokenService.GetToken(user.UserName);
                return Ok(new AuthorizedUserDto
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = token,
                    EmailConfirmed = user.EmailConfirmed,
                    ImgUrl = user.ImgUrl
                });
            }
            else
            {
                return BadRequest("Invalid login data");
            }

        }
        /// <summary>
        /// Manages user registration process, ensuring uniqueness of email and username.
        /// </summary>
        /// <param name="registerDto">instance of data transfer object that contains registration credentials</param>
        /// <returns>Status code of operation</returns>
        /// <response code="200">If user has been succesfully registered</response>
        /// <response code="400">If user cannot be registered</response>
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterDto registerDto)
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

                HandleEmail(newUser, false, " ");

                return Ok("Succesfully registered, check your mailbox");
            }
        }
        /// <summary>
        /// Handles the process of resetting user passwords by validating email confirmation URL and updating password.
        /// </summary>
        /// <param name="userId">id of user that wants to reset password</param>
        /// <param name="code">generated password reset token (available only after clicking link retrieved on email)</param>
        /// <param name="newPassword">new password for user</param>
        /// <returns>Status code of operation</returns>
        /// <response code="200">If email with password reset token has been sent</response>
        /// <response code="400">If sending email went wrong or email is not valid</response>
        [HttpGet("forgetpassword")]
        public async Task<ActionResult> ForgetPassword([FromQuery] string userId, [FromQuery] string code, [FromQuery] string newPassword)
        {
            if (userId == null || code == null) return BadRequest("Invalid email confirmation url");
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return BadRequest("Invalid email parameter");
            var result = await _userManager.ResetPasswordAsync(user, code, newPassword);
            if (result.Succeeded) return Ok("Success! You can go back to page");
            return BadRequest("Please try again later");
        }
        /// <summary>
        /// Manages email confirmation process by validating confirmation URL.
        /// </summary>
        /// <param name="userId">id of user that want to confirm email</param>
        /// <param name="code">generated verification token (available only after clicking link retrieved on email)</param>
        /// <returns>Status code of operation</returns>
        /// <response code="200">If email has been confirmed sucessfully</response>
        /// <response code="400">If email cannot be confirmed or email url is invalid</response>
        [HttpGet("confirmemail")]
        public async Task<ActionResult> ConfirmEmail([FromQuery] string userId, [FromQuery] string code)
        {
            if (userId == null || code == null) return BadRequest("Invalid email confirmation url");
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return BadRequest("Invalid email parameter");

            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded) return Ok("Success! You can go back to page");
            return BadRequest("Please try again later");

        }
        /// <summary>
        /// Initiates the process of sending verification emails for either email confirmation or password reset.
        /// </summary>
        /// <param name="email">email address where email will be sent</param>
        /// <param name="isPassword">true if it's password email, otherwise false</param>
        /// <param name="password">user password</param>
        /// <returns>Status code of operation</returns>
        /// <response code="200">If email has been sent</response>
        [HttpPost("sendEmail")]
        public async Task<ActionResult> GetNewVerifyEmail([FromQuery] string email, [FromQuery] bool isPassword, [FromQuery] string password = " ")
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return NotFound("Email not found");
            if (user.EmailConfirmed && !isPassword) return Ok("Email is Already verified");
            HandleEmail(user, isPassword, password);
            return Ok("Email has been sent, check your inbox. If you don't see verification Email, check your spam");
        }
        /// <summary>
        ///  Retrieves the current user details.
        /// </summary>
        /// <returns>Status code of operation with current user data</returns>
        /// <response code="200">If current user has been retrieved from database</response>
        /// <response code="404">If current user is not found</response>
        [HttpGet("{currentUser}")]
        public async Task<ActionResult<UserClientDto>> GetCurrentUser()
        {
            var UserName = User.GetUsernameFromToken();
            var user = await _userManager.Users.Include(u => u.Followed).Include(u => u.Followers).Include(u => u.Notifications).FirstOrDefaultAsync(u => u.UserName == UserName);
            var Notifications = await _unitOfWork.NotificationRepository.GetNotifications(user);
            if (user == null) return NotFound("User not found");
            return Ok(new UserClientDto
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id,
                Bio = user.Bio,
                ImgUrl = user.ImgUrl,
                Followed = _mapper.Map<List<FollowDto>>(user.Followed),
                Followers = _mapper.Map<List<FollowDto>>(user.Followers)

            });
        }
        /// <summary>
        /// Private method to manage the sending of email notifications for password reset or email confirmation.
        /// </summary>
        /// <param name="newUser">instance of user that got email</param>
        /// <param name="isPassword">true if it's password email, otherwise false</param>
        /// <param name="password">new password for user</param>
        private async void HandleEmail(User newUser, bool isPassword, string password)
        {

            if (isPassword)
            {
                var email_body = GetHtmlBody.GetBodyForResetPassword();
                var code = await _userManager.GeneratePasswordResetTokenAsync(newUser);
                var callback = Request.Scheme + "://" + Request.Host + Url.Action("forgetpassword", "Auth", new { userId = newUser.Id, code = code, newPassword = password });
                var body = email_body.Replace("#URL#", System.Text.Encodings.Web.HtmlEncoder.Default.Encode(callback));
                _emailSender.SendEmail(body, newUser.Email, "Reset your password");
            }
            else
            {
                var email_body = GetHtmlBody.GetBodyForConfirm();
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                var callback = Request.Scheme + "://" + Request.Host + Url.Action("confirmEmail", "Auth", new { userId = newUser.Id, code = code });
                var body = email_body.Replace("#URL#", System.Text.Encodings.Web.HtmlEncoder.Default.Encode(callback));
                _emailSender.SendEmail(body, newUser.Email, "Confirm your email");
            }



        }
        /// <summary>
        /// Private method to validate the format of an email address using regular expressions.
        /// </summary>
        /// <param name="email">Email to check if is valid</param>
        /// <returns>Boolean value that is true when email has valid format</returns>
        private bool IsEmailValid(string email)
        {

            string pattern = @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$";


            return Regex.IsMatch(email, pattern);
        }

    }
}