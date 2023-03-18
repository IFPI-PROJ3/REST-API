namespace Proj3.Contracts.Authentication.Request;

public record AddPhoneNumberRequest(
    string UserId,
    string PhoneNumber
);