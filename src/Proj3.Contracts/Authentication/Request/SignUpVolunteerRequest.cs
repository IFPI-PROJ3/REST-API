namespace Proj3.Contracts.Authentication.Request;

public record SignUpVolunteerRequest(
    string username,
    string email,
    string password,
    string name,
    string lastname,
    string description,
    string latitude,
    string longitude,
    List<int> categories
);
