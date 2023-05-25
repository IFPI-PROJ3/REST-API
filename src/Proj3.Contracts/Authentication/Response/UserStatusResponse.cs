namespace Proj3.Contracts.Authentication.Response;

public record UserStatusResponse(
    Guid id,    
    string username,    
    string email,
    bool active
);