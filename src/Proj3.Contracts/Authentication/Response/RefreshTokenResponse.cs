namespace Proj3.Contracts.Authentication.Response;

public record RefreshTokenResponse(    
    string acess_token,
    string refresh_token
);