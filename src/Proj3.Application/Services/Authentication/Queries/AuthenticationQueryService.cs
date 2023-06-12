using Proj3.Application.Common.Errors.Authentication;
using Proj3.Application.Common.Interfaces.Persistence.Authentication;
using Proj3.Application.Common.Interfaces.Services.Authentication.Queries;
using Proj3.Application.Common.Interfaces.Utils.Authentication;
using Proj3.Application.Services.Authentication.Result;
using Proj3.Application.Utils.Authentication;
using Proj3.Contracts.Authentication.Request;
using Proj3.Domain.Entities.Authentication;
using System.Security.Claims;

namespace Proj3.Application.Services.Authentication.Queries
{
    public class AuthenticationQueryService : IAuthenticationQueryService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokensUtils _tokensUtils;
        private readonly IRefreshTokenRepository _refreshTokensRepository;
        private readonly IUserValidationCodeRepository _userValidationCodeRepository;

        public AuthenticationQueryService(IUserRepository userRepository, ITokensUtils tokensUtils, IRefreshTokenRepository refreshTokenRepository, IUserValidationCodeRepository userValidationCodeRepository)
        {
            _tokensUtils = tokensUtils;
            _userRepository = userRepository;
            _refreshTokensRepository = refreshTokenRepository;
            _userValidationCodeRepository = userValidationCodeRepository;
        }

        public async Task<AuthenticationResult> SignIn(SignInRequest signInRequest)
        {
            if (!(await _userRepository.GetUserByEmail(signInRequest.email) is Domain.Entities.Authentication.User user && user.PasswordHash == Crypto.ReturnUserHash(user, signInRequest.password)))
            {
                throw new InvalidCredentialsException();
            }
            
            string? accessToken = _tokensUtils.GenerateJwtToken(user);
            ClaimsPrincipal claimsPrincipal = _tokensUtils.ExtractClaimsFromToken(accessToken);
            
            RefreshToken? refreshToken = _tokensUtils.GenerateRefreshToken(user, claimsPrincipal);

            await _refreshTokensRepository.Add(refreshToken);

            return new AuthenticationResult(
                user, 
                accessToken, 
                refreshToken.Token
            );
        }                
    }
}