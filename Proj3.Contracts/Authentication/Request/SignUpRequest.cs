namespace Proj3.Contracts.Authentication.Request;

public record SignUpRequest(
    string UserName,
    string Email,
    string Password
);