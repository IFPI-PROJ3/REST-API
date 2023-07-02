using Proj3.Domain.Entities.NGO;

namespace Proj3.Contracts.NGO.Response;

public record NgoPageInfo(
    Ngo ngo,
    List<string> categories,
    float average_rating,
    List<EventToCard> upcomming_events,
    List<EventToCard> active_events,
    List<EventToCard> ended_events
);

