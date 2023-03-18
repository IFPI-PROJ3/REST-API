namespace Proj3.Contracts.Authentication.Request;

public record ChangePasswordRequest(
    string userId,
    string NewPassword
);