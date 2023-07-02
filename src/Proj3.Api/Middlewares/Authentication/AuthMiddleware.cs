using Proj3.Application.Common.Interfaces.Utils.Authentication;
using Proj3.Application.Common.Interfaces.Persistence.Authentication;
using Proj3.Domain.Entities.Authentication;

namespace Proj3.Api.Middlewares.Authentication
{
    ///
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        
        ///
        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        ///
        public async Task Invoke(HttpContext context, IUserRepository userRepository, IRefreshTokenRepository tokenRepository, ITokensUtils tokensUtils)
        {
            if(context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last() is not string token)
            {
                throw new UnauthorizedAccessException("Request without token.");
            }
            
            if(tokensUtils.ValidateJwtToken(token!) is not Guid userId || 
                await userRepository.GetUserByIdAsync(userId) is not User user ||
                tokenRepository.GetAllUsersRefreshTokensAsync(userId).Result.FirstOrDefault()!.Expires < DateTime.Now)
            {
                throw new UnauthorizedAccessException("Expired, invalid or revoked token.");
            }

            if (!user.Active)
            {
                throw new UnauthorizedAccessException("This account is not active.");
            }

            context.User = tokensUtils.ExtractClaimsFromToken(token!);            
            await _next(context);
           
        }
    }
}