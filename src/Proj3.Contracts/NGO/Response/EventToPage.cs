using Proj3.Domain.Entities.Common;
using Proj3.Domain.Entities.Volunteer;

namespace Proj3.Contracts.NGO.Response;

public record EventToPage
(
    Guid id,
    Guid ngo_id,
    string title,
    string description,
    bool quick_event,
    int volunteers_limit,
    int volunteers_count,
    DateTime start_date,
    DateTime end_date,    
    DateTime created_at,
    DateTime? updated_at,
    List<string> categories,
    List<Review> reviews,
    string? image_thumb    
);
