using Proj3.Contracts.NGO.Response;

namespace Proj3.Contracts.Volunteer.Response;

public record VolunteerPageInfo(
    Domain.Entities.Volunteer.Volunteer volunteer,
    List<string> categories,
    List<EventToCard> upcomming_events,
    List<EventToCard> active_events,
    List<EventToCard> ended_events
);
