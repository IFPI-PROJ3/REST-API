namespace Proj3.Contracts.Authentication.Request;

public record RefreshTokenRequest(
    string access_token,
    string refresh_token    
);