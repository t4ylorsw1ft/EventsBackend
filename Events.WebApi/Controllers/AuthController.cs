using AutoMapper;
using Events.Application.Auth.Login;
using Events.Application.Auth.Logout;
using Events.Application.Auth.Refresh;
using Events.WebApi.Models.Auth;
using Microsoft.AspNetCore.Mvc;


namespace Events.WebApi.Controllers
{
    /// <summary>
    /// Controller responsible for handling authentication-related operations.
    /// </summary>
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        private readonly IMapper _mapper;
        public AuthController(IMapper mapper) => _mapper = mapper;

        /// <summary>
        /// Authenticates a user and generates access and refresh tokens.
        /// </summary>
        /// <param name="loginDto">The data transfer object containing login credentials.</param>
        /// <returns>Returns a success message if authentication is successful.</returns>
        /// <remarks>
        /// Sets the access and refresh tokens as cookies in the response.
        /// </remarks>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var command = _mapper.Map<LoginCommand>(loginDto);
            var tokens = await Mediator.Send(command);

            HttpContext.Response.Cookies.Append("access-token", tokens.AccessToken);
            HttpContext.Response.Cookies.Append("refresh-token", tokens.RefreshToken);

            return Ok("success");
        }

        /// <summary>
        /// Refreshes the access token using the provided refresh token.
        /// </summary>
        /// <returns>Returns a success message if the token refresh is successful.</returns>
        /// <remarks>
        /// Reads the refresh token from cookies and issues a new access token and refresh token.
        /// </remarks>
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh()
        {
            var refreshToken = Request.Cookies["refresh-token"];

            var command = new RefreshCommand
            {
                RefreshToken = refreshToken,
            };

            var tokens = await Mediator.Send(command);

            HttpContext.Response.Cookies.Append("access-token", tokens.AccessToken);
            HttpContext.Response.Cookies.Append("refresh-token", tokens.RefreshToken);

            return Ok("success");
        }

        /// <summary>
        /// Logs out the user and clears authentication tokens.
        /// </summary>
        /// <returns>Returns a success message if logout is successful.</returns>
        /// <remarks>
        /// Deletes the access token and refresh token cookies to log out the user.
        /// </remarks>
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var command = new LogoutCommand
            {
                Id = UserId,
            };

            await Mediator.Send(command);

            HttpContext.Response.Cookies.Delete("access-token");
            HttpContext.Response.Cookies.Delete("refresh-token");
            return Ok("success");
        }
    }
}

