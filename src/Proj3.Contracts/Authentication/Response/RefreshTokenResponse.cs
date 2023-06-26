namespace Proj3.Contracts.Authentication.Response;

public record RefreshTokenResponse(    
    string access_token,
    string refresh_token
);