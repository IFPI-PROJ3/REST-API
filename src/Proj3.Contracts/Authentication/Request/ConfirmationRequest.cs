namespace Proj3.Contracts.Authentication.Request;

public record ConfirmationRequest(
    Guid user_id,
    int code
);