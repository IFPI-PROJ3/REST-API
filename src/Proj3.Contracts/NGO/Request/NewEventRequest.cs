namespace Proj3.Contracts.NGO.Request
{
    public record NewEventRequest
    (
        string title,
        string description,
        bool quick_event,
        int volunteers_limit,
        DateTime start_date,
        DateTime end_date
    );
}
