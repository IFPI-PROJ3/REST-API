namespace Proj3.Contracts.Authentication.Response;

public record AuthenticationResponse(
    Guid id,
    string username,
    string email,    
    bool active,
    string access_token,
    string refresh_token
);