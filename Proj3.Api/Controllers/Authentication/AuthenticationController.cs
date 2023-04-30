using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proj3.Application.Common.Interfaces.Services.Authentication.Command;
using Proj3.Application.Common.Interfaces.Services.Authentication.Commands;
using Proj3.Application.Common.Interfaces.Services.Authentication.Queries;
using Proj3.Application.Services.Authentication.Result;
using Proj3.Contracts.Authentication.Request;
using Proj3.Contracts.Authentication.Response;

namespace Proj3.Api.Controllers.Authentication
{
    [ApiController]
    [AllowAnonymous]
    [Route("auth")]
    [ApiVersion("1.0")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationCommandService _authenticationCommandService;
        private readonly IAuthenticationQueryService _authenticationQueryService;
        private readonly IEmailCommandService _emailCommandService;

        public AuthenticationController(IAuthenticationCommandService authenticationCommandService, IAuthenticationQueryService authenticationQueryService, IEmailCommandService emailCommandService)
        {
            _authenticationCommandService = authenticationCommandService;
            _authenticationQueryService = authenticationQueryService;
            _emailCommandService = emailCommandService;
        }

        /// <summary>
        /// Ngo user signup
        /// </summary>
        /// <param name="request">User data</param>
        /// <example></example>
        /// <returns></returns>
        /// <response code="200">User created</response>
        /// <response code="409">User already exists</response>
        /// <response code="422">User validation error</response> 
        /// <response code="500">Server internal error</response>
        [ProducesResponseType(typeof(UserStatusResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)] 
        [HttpPost("signup-ngo")]
        public async Task<IActionResult> SignUpNgo([FromQuery]SignUpRequest request)
        {
            UserStatusResult? userInactiveResult = await _authenticationCommandService.SignUpNgo(
                request.username,
                request.email,
                request.password
            );

            UserStatusResponse? response = new
            (
                userInactiveResult.user.Id,
                userInactiveResult.user.UserName,
                userInactiveResult.user.Email,
                userInactiveResult.user.Active
            );

            return Ok(response);
        }

        /// <summary>
        /// Volunteer user signup
        /// </summary>
        /// <param name="request">User info</param>
        /// <example></example>
        /// <returns></returns>
        /// <response code="200">User created</response>
        /// <response code="409">User already exists</response>
        /// <response code="422">User validation error</response>        
        /// <response code="500">Server internal error</response>
        [ProducesResponseType(typeof(UserStatusResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPost("signup-volunteer")]
        public async Task<IActionResult> SignUpVolunteer([FromQuery]SignUpRequest request)
        {
            UserStatusResult? userInactiveResult = await _authenticationCommandService.SignUpVolunteer(
                request.username,
                request.email,
                request.password
            );

            UserStatusResponse? response = new
            (
                userInactiveResult.user.Id,
                userInactiveResult.user.UserName,
                userInactiveResult.user.Email,
                userInactiveResult.user.Active
            );

            return Ok(response);
        }

        /// <summary>
        /// User signin
        /// </summary>
        /// <param name="request">User data</param>
        /// <example></example>
        /// <returns></returns>
        /// <response code="200">User authenticated</response>
        /// <response code="401">Invalid credentials</response>   
        /// <response code="500">Server internal error</response>
        [ProducesResponseType(typeof(AuthenticationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("signin")]
        public async Task<IActionResult> SignIn([FromQuery]SignInRequest request)
        {
            AuthenticationResult? authServiceResult = await _authenticationQueryService.SignIn(
                request.email,
                request.password
            );

            AuthenticationResponse? response = new
            (
                id: authServiceResult.user.Id,
                username: authServiceResult.user.UserName,
                email: authServiceResult.user.Email,
                active: authServiceResult.user.Active,
                access_token: authServiceResult.AccessToken,
                authServiceResult.RefreshToken
            );
            
            return Ok(response);
        }

        /// <summary>
        /// Receive new refresh token        
        /// </summary>        
        /// <param name="request">Tokens</param>
        /// <example></example>
        /// <returns></returns>
        /// <response code="200">User authenticated</response>
        /// <response code="401">Access token is invalid</response>
        /// <response code="401">Refresh token is invalid</response>
        /// <response code="500">Server internal error</response>
        [ProducesResponseType(typeof(AuthenticationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]        
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("refresh-token")]
        public ActionResult RefreshToken([FromQuery]RefreshTokenRequest request)
        {
            AuthenticationResult? authResult = _authenticationQueryService.RefreshToken(
                request.refresh_token,
                request.access_token
            );

            RefreshTokenResponse? refreshTokenResponse = new
            (
                authResult.RefreshToken,
                authResult.AccessToken
            );

            return Ok(refreshTokenResponse);
        }

        /// <summary>
        /// Change password
        /// </summary>
        /// <param name="request">User credentials</param>
        /// <example></example>
        /// <returns></returns>
        /// <response code="200">User authenticated</response>
        /// <response code="401">Invalid credentials exception</response>
        /// <response code="401">Invalid password exception</response>
        /// <response code="500">Server internal error</response>
        [ProducesResponseType(typeof(AuthenticationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword([FromQuery]ChangePasswordRequest request)
        {
            AuthenticationResult? authServiceResult = await _authenticationCommandService.ChangePassword(
                email: request.email,
                oldPassword: request.old_password,
                newPassword: request.new_password
            );

            AuthenticationResponse? response = new
            (
                id: authServiceResult.user.Id,
                username: authServiceResult.user.UserName,
                email: authServiceResult.user.Email,
                active: authServiceResult.user.Active,
                access_token: authServiceResult.AccessToken,
                refresh_token: authServiceResult.RefreshToken
            );

            return Ok(response);
        }

        ///// <summary>
        ///// Forgot Password
        ///// </summary>        
        //[HttpPut("forgot-password")]
        //public async Task<IActionResult> ForgotPassword([FromQuery]ForgotPasswordRequest request)
        //{            
        //    _emailCommandService.SendResetPasswordEmail(request.email);
        //    return Ok(StatusCodes.Status200OK);
        //}

        ///// <summary>
        ///// Email confirmation
        ///// </summary>
        ///// <param name="request">Email</param>        
        //[HttpPut("email-confirmation")]
        //public async Task<IActionResult> EmailConfirmation([FromQuery] ConfirmationRequest request)
        //{
        //    UserStatusResult? authServiceResult = await _authenticationCommandService.ConfirmEmail(
        //        Guid.Parse(request.user_id),
        //        request.code
        //    );

        //    UserStatusResponse? response = new
        //    (
        //        id: authServiceResult.user.Id,
        //        username: authServiceResult.user.UserName,
        //        email: authServiceResult.user.Email,
        //        active: authServiceResult.user.Active
        //    );

        //    return Ok(response);
        //}
    }
}

