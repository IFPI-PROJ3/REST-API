using Microsoft.AspNetCore.Mvc;
using Proj3.Application.Common.Interfaces.Services.Authentication.Command;
using Proj3.Application.Common.Interfaces.Services.Authentication.Queries;
using Proj3.Contracts.Authentication.Request;
using Proj3.Contracts.Authentication.Response;
using Proj3.Application.Services.Authentication.Result;
using Proj3.Application.Common.Errors.Authentication;
using System.Net;

namespace Proj3.Api.Controllers.Authentication
{
    [ApiController]
    [Route("auth")]
    [ApiVersion("1.0")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationCommandService _authenticationCommandService;
        private readonly IAuthenticationQueryService _authenticationQueryService;

        public AuthenticationController(IAuthenticationCommandService authenticationCommandService, IAuthenticationQueryService authenticationQueryService)
        {
            _authenticationCommandService = authenticationCommandService;
            _authenticationQueryService = authenticationQueryService;
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
        //[ProducesResponseType(typeof(UserAlreadyExistsException), StatusCodes.Status409Conflict)]
        //[ProducesResponseType(typeof(InvalidEmailException), StatusCodes.Status422UnprocessableEntity)]
        //[ProducesResponseType(typeof(InvalidPasswordException), StatusCodes.Status422UnprocessableEntity)]
        //[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPost("signup-ngo")]
        public async Task<IActionResult> SignUpNgo(SignUpRequest request)
        {
            UserStatusResult? userInactiveResult = await _authenticationCommandService.SignUpNgo(
                request.UserName,
                request.Email,
                request.Password
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
        /// <param name="request">User data</param>
        /// <example></example>
        /// <returns></returns>
        /// <response code="200">User created</response>
        /// <response code="409">User already exists</response>
        /// <response code="422">User validation error</response>        
        /// <response code="500">Server internal error</response>
        [ProducesResponseType(typeof(UserStatusResponse), StatusCodes.Status200OK)]
        [HttpPost("signup-volunteer")]
        public async Task<IActionResult> SignUpVolunteer(SignUpRequest request)
        {
            UserStatusResult? userInactiveResult = await _authenticationCommandService.SignUpVolunteer(
                request.UserName,
                request.Email,
                request.Password
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
        /// Signin
        /// </summary>
        /// <param name="request">User data</param>
        /// <example></example>
        /// <returns></returns>
        /// <response code="200">User created</response>
        /// <response code="409">User already exists</response>
        /// <response code="422">User validation error</response>        
        /// <response code="500">Server internal error</response>
        [ProducesResponseType(typeof(UserStatusResponse), StatusCodes.Status200OK)]
        [HttpGet("signin")]
        public async Task<IActionResult> SignIn(SignInRequest request)
        {
            AuthenticationResult? authServiceResult = await _authenticationQueryService.SignIn(
                request.Email,
                request.Password
            );

            AuthenticationResponse? response = new
            (
                authServiceResult.user.Id,
                authServiceResult.user.UserName,
                authServiceResult.user.Email,                
                authServiceResult.AcessToken,
                authServiceResult.RefreshToken
            );
            
            return Ok(response);
        }

        [HttpGet("refresh-token")]
        public ActionResult RefreshToken(RefreshTokenRequest request)
        {
            AuthenticationResult? authResult = _authenticationQueryService.RefreshToken(
                request.RefreshToken,
                request.AccessToken
            );

            RefreshTokenResponse? refreshTokenResponse = new
            (
                authResult.RefreshToken,
                authResult.AcessToken
            );

            return Ok(refreshTokenResponse);
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
        {
            AuthenticationResult? authServiceResult = await _authenticationCommandService.ChangePassword(
                Guid.Parse(request.userId),
                request.NewPassword
            );

            AuthenticationResponse? response = new
            (
                authServiceResult.user.Id,
                authServiceResult.user.UserName,
                authServiceResult.user.Email,                
                authServiceResult.AcessToken,
                authServiceResult.RefreshToken
            );
            return Ok(response);
        }

        [HttpPost("email-confirmation")]
        public async Task<IActionResult> EmailConfirmation(ConfirmationRequest request)
        {
            UserStatusResult? authServiceResult = await _authenticationCommandService.ConfirmEmail(
                Guid.Parse(request.userId),
                request.code
            );

            UserStatusResponse? response = new
            (
                authServiceResult.user.Id,
                authServiceResult.user.UserName,
                authServiceResult.user.Email,
                authServiceResult.user.Active
            );

            return Ok(response);
        }        
    }
}

