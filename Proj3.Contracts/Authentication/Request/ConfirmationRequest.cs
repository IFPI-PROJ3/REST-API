namespace Proj3.Contracts.Authentication.Request;

public record ConfirmationRequest(
    string userId,
    int code
);