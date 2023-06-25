namespace Proj3.Contracts.Authentication.Request;

public record SignUpVolunteerRequest(
    string username,
    string email,
    string password,
    string name,
    string lastname,
    string description,
    double latitude,
    double longitude,
    List<int> categories
);
