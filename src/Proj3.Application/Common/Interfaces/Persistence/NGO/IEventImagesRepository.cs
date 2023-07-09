using Proj3.Domain.Entities.NGO;

namespace Proj3.Application.Common.Interfaces.Persistence.NGO
{
    public interface IEventImagesRepository
    {
        public Task<EventImage> AddOrUpdateThumbAsync(byte[] image, EventImage eventImage);

        public Task<List<EventImage>> AddImagesAsync(List<Tuple<byte[], EventImage>> eventImages);

        public Task<EventImage?> GetThumbImageAsync(Guid eventId);

        public Task<List<EventImage>> GetEventImagesAsync(Guid eventId);

        public Task DeleteEventImageAsync(Guid eventImageId);
    }
}
