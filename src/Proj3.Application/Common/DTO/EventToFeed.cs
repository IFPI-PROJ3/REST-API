using Proj3.Domain.Entities.Common;

namespace Proj3.Application.Common.DTO;
    public record EventToFeed
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
        List<Category> categories
    );

