using Proj3.Application.Common.Errors.Authentication;
using Proj3.Application.Common.Interfaces.Persistence.Authentication;
using Proj3.Application.Common.Interfaces.Services.Authentication.Queries;
using Proj3.Application.Common.Interfaces.Utils.Authentication;
using Proj3.Application.Services.Authentication.Result;
using Proj3.Application.Utils.Authentication;
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

        public async Task<AuthenticationResult> SignIn(string email, string password)
        {
            if (!(await _userRepository.GetUserByEmail(email) is User user && user.PasswordHash == Crypto.ReturnUserHash(user, password)))
            {
                throw new InvalidCredentialsException();
            }
            
            string? accessToken = _tokensUtils.GenerateJwtToken(user);
            ClaimsPrincipal claimsPrincipal = _tokensUtils.ExtractClaimsFromToken(accessToken);
            
            RefreshToken? refreshToken = _tokensUtils.GenerateRefreshToken(user, claimsPrincipal);

            await _refreshTokensRepository.Add(refreshToken);

            return new AuthenticationResult(user, accessToken, refreshToken.Token);
        }

        public AuthenticationResult RefreshToken(string refreshtoken, string acesstoken)
        {
            if (_tokensUtils.ValidateJwtToken(acesstoken) is null)
            {
                throw new Exception("Invalid access token.");
            }
            if (_refreshTokensRepository.GetByToken(refreshtoken).Result is not RefreshToken rf)
            {
                throw new Exception("Invalid refresh token.");
            }

            User user = _userRepository.GetUserById(rf.UserId).Result!;        
            ClaimsPrincipal claimsPrincipal = _tokensUtils.ExtractClaimsFromToken(acesstoken);
        
            string newAccessToken = _tokensUtils.GenerateJwtToken(user, claimsPrincipal);
            RefreshToken newRefreshToken = _tokensUtils.GenerateRefreshToken(user, claimsPrincipal);

            _refreshTokensRepository.Update(newRefreshToken);

            return new AuthenticationResult(user, newAccessToken, newRefreshToken.Token);
        }        
    }
}