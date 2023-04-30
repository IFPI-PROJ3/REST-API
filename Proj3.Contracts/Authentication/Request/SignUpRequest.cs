namespace Proj3.Contracts.Authentication.Request;

public record SignUpRequest(
    string username,
    string email,
    string password
);