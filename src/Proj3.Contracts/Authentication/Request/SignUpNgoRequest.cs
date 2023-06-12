namespace Proj3.Contracts.Authentication.Request;

public record SignUpNgoRequest(    
    string username,
    string email,
    string password,
    string name,
    string description,
    double latitude,
    double longitude,
    List<int> categories
);