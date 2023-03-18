namespace Proj3.Contracts.Authentication.Response;

public record RefreshTokenResponse(    
    string AcessToken,
    string RefreshToken
);