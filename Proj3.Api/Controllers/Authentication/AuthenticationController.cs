using Microsoft.AspNetCore.Mvc;
using Proj3.Application.Common.Interfaces.Services.Authentication.Command;
using Proj3.Application.Common.Interfaces.Services.Authentication.Queries;
using Proj3.Contracts.Authentication.Request;
using Proj3.Contracts.Authentication.Response;
using Proj3.Application.Services.Authentication.Result;

namespace Proj3.Api.Controllers.Authentication
{
    [ApiController]
    [Route("auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationCommandService _authenticationCommandService;
        private readonly IAuthenticationQueryService _authenticationQueryService;

        public AuthenticationController(IAuthenticationCommandService authenticationCommandService, IAuthenticationQueryService authenticationQueryService)
        {
            _authenticationCommandService = authenticationCommandService;
            _authenticationQueryService = authenticationQueryService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(SignUpRequest request)
        {
            UserStatusResult? userInactiveResult = await _authenticationCommandService.SignUp(
                request.Name,
                request.Email,
                request.Password
            );

            UserStatusResponse? response = new
            (
                userInactiveResult.user.Id,
                userInactiveResult.user.Name,
                userInactiveResult.user.Email,
                userInactiveResult.user.ActiveAccount
            );

            return Ok(response);
        }

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
                authServiceResult.user.Name,
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
                authServiceResult.user.Name,
                authServiceResult.user.Email,                
                authServiceResult.AcessToken,
                authServiceResult.RefreshToken
            );
            return Ok(response);
        }

        [HttpPost("email-confirmation")]
        public async Task<IActionResult> EmailConfirmation(ConfirmationRequest request)
        {
            UserStatusResult? authServiceResult = await _authenticationQueryService.ConfirmEmail(
                Guid.Parse(request.userId),
                request.code
            );

            UserStatusResponse? response = new
            (
                authServiceResult.user.Id,
                authServiceResult.user.Name,
                authServiceResult.user.Email,
                authServiceResult.user.ActiveAccount
            );

            return Ok(response);
        }

        [HttpPost("add-phone-number")]
        public async Task<IActionResult> AddPhoneNumber(AddPhoneNumberRequest request)
        {
            await _authenticationCommandService.AddPhoneNumber(
                Guid.Parse(request.UserId),
                request.PhoneNumber
            );

            return Ok("Phonenumber registered for confirmation.");
        }

        [HttpPost("phone-number-confirmation")]
        public async Task<IActionResult> PhoneNumberConfirmation(ConfirmationRequest request)
        {
            UserStatusResult? authServiceResult = await _authenticationQueryService.ConfirmPhoneNumber(
                Guid.Parse(request.userId),
                request.code
            );

            UserStatusResponse? response = new
            (
                authServiceResult.user.Id,
                authServiceResult.user.Name,
                authServiceResult.user.Email,
                authServiceResult.user.ActiveAccount
            );

            return Ok(response);
        }
    }
}

