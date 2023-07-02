namespace Proj3.Contracts.NGO.Request
{
    public record EventsFeedRequest(
        List<int> categories,
        int days_to_event
    );    
}
