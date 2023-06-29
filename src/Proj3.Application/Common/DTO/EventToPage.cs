using Proj3.Domain.Entities.Common;
using Proj3.Domain.Entities.Volunteer;

namespace Proj3.Application.Common.DTO;
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
    bool cancelled,
    DateTime created_at,
    DateTime? updated_at,
    List<Category> categories,
    List<Review> reviews
    //List<images>;
);
