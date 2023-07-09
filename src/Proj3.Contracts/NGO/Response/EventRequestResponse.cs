namespace Proj3.Contracts.NGO.Response;

public record EventRequestResponse
(
    Guid id,
    Guid event_id,
    Guid volunteer_id,
    string volunteer_name,
    string volunteer_email,
    string profile_image
);

