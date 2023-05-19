namespace Proj3.Contracts.Authentication.Request;

public record ChangePasswordRequest(
    string email,
    string old_password,
    string new_password
);