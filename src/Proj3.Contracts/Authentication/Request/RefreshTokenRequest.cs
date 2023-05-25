namespace Proj3.Contracts.Authentication.Request;

public record RefreshTokenRequest(
    string refresh_token,
    string access_token
);