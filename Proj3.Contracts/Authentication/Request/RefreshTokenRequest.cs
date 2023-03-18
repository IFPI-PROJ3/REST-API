namespace Proj3.Contracts.Authentication.Request;

public record RefreshTokenRequest(
    string RefreshToken,
    string AccessToken
);