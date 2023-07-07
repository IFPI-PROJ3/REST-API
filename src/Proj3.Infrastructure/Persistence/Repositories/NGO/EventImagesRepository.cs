using Microsoft.EntityFrameworkCore;
using Proj3.Application.Common.Interfaces.Persistence;
using Proj3.Application.Common.Interfaces.Persistence.NGO;
using Proj3.Domain.Entities.NGO;
using Proj3.Infrastructure.Services;

namespace Proj3.Infrastructure.Persistence.Repositories.NGO
{
    public class EventImagesRepository : IEventImagesRepository
    {
        private readonly IRepositoryBase<EventImage> _repository;

        public EventImagesRepository(IRepositoryBase<EventImage> repository)
        {
            _repository = repository;
        }

        public async Task<EventImage> AddThumbAsync(byte[] image, EventImage eventImage)
        {
            try
            {
                await S3Service.FileUploadAsync(image, eventImage.Id.ToString()+".jpg");
                return await _repository.AddAsync(eventImage);                
            }
            catch (Exception)
            {
                await S3Service.DeleteFileAsync(eventImage.Id.ToString()+".jpg");
                throw;
            }
        }

        public async Task<List<EventImage>> AddImagesAsync(List<Tuple<byte[], EventImage>> eventImages)
        {
            List<EventImage> eventImagesAdded = new List<EventImage>();

            for (int i = 0; i > eventImages.Count; i++)
            {
                try
                {
                    await S3Service.FileUploadAsync(eventImages[i].Item1, eventImages[i].Item2.Id.ToString()+".jpg");

                    await _repository.AddAsync(eventImages[i].Item2);
                    eventImagesAdded.Add(eventImages[i].Item2);
                }
                catch (Exception)
                {
                    await S3Service.DeleteFileAsync(eventImages[i].Item2.Id.ToString()+".jpg");
                    throw;
                }                                               
            }

            return eventImagesAdded;
        }        

        public async Task<EventImage?> GetThumbImageAsync(Guid eventId)
        {
             return await _repository.Entity.Where(e => e.EventId == eventId).FirstOrDefaultAsync();
        }

        public async Task<List<EventImage>> GetEventImagesAsync(Guid eventId)
        {
            return await _repository.Entity.Where(e => e.EventId == eventId).ToListAsync();
        }

        public async Task DeleteEventImageAsync(Guid eventImageId)
        {
            await _repository.DeleteAsync(eventImageId+".jpg");
        }
    }
}
