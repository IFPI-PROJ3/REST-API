namespace Proj3.Contracts.Authentication.Response;

public record UserStatusResponse(
    Guid Id,
    string Name,
    string Email,
    bool ActiveAccount
);