using Proj3.Domain.Entities.Common;

namespace Proj3.Contracts.NGO.Response;

public record EventToCard
(
    Guid id, 
    Guid ngo_id,
    string title,
    string description,
    bool quick_event,
    int volunteers_limit,  
    int requests_count,
    int volunteers_count,
    DateTime start_date,
    DateTime end_date,         
    DateTime created_at,
    DateTime? updated_at,
    List<string> categories,
    string? image_thumb
);

public record EventToCardVolunteer
(
    Guid id,
    Guid ngo_id,
    string title,
    string description,
    bool quick_event,
    int volunteers_limit,
    int requests_count,
    int volunteers_count,
    DateTime start_date,
    DateTime end_date,
    DateTime created_at,
    DateTime? updated_at,
    List<string> categories,
    string? image_thumb,
    bool can_be_reviewed
);