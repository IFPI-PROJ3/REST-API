namespace Proj3.Contracts.Authentication.Request;

public record SignInRequest(
    string Email,
    string Password
);