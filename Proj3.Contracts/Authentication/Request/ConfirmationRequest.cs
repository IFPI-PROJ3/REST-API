namespace Proj3.Contracts.Authentication.Request;

public record ConfirmationRequest(
    string user_id,
    int code
);