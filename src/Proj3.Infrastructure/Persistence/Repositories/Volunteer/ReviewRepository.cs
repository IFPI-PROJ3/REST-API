using Amazon.S3.Model;
using Microsoft.EntityFrameworkCore;
using Proj3.Application.Common.Interfaces.Persistence;
using Proj3.Application.Common.Interfaces.Persistence.NGO;
using Proj3.Application.Common.Interfaces.Persistence.Volunteer;
using Proj3.Domain.Entities.Volunteer;

namespace Proj3.Infrastructure.Persistence.Repositories.Volunteer
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly IRepositoryBase<Review> _repository;
        private readonly IEventRepository _eventRepository;

        // TO DO CORRIGIR DEPOIS
        public ReviewRepository(IRepositoryBase<Review> repository, IEventRepository eventRepository)
        {
            _repository = repository;
            _eventRepository = eventRepository;
        }
        
        public async Task<List<Review>> GetReviewsByEvent(Guid eventId)
        {
            return await _repository.Entity.Where(r => r.EventId == eventId).ToListAsync();
        }

        public async Task<List<Review>> GetReviewsByNgo(Guid ngoId)
        {
            List<Review> reviews = new();

            var endedEvents = await _eventRepository.GetEndedEventsByNgoAsync(ngoId).ToListAsync();

            foreach(var endedEvent in endedEvents)
            {
                var eventReviews = await _repository.Entity.Where(r => r.EventId == endedEvent.Id).ToListAsync();
                reviews.AddRange(eventReviews);
            }
            
            return (List<Review>)reviews.Take(10);
        }

        public async Task<float> GetAverageRatingByEvent(Guid eventId)
        {
            float[] averageRating = Array.Empty<float>();

            var reviewEvents = _repository.Entity.Where(r => r.EventId == eventId).AsAsyncEnumerable();            

            await foreach (Review review in reviewEvents)
            {
                _ = averageRating.Append(review.Stars);
            }

            return averageRating.Average();
        }

        public async Task<bool> UserAlreadyPostReviewAsync(Guid eventId, Guid volunteerId)
        {
            return await _repository.Entity.Where(r => r.EventId == eventId && r.VolunteerId == volunteerId).AnyAsync();
        }

        public Task<Review> AddAsync(Review review)
        {
            return _repository.AddAsync(review);
        }

        public async Task<bool> UpdateAsync(Review review)
        {
            return await _repository.UpdateAsync(review);
        }

        public async Task DeleteAsync(Guid EventId, Guid VolunteerId)
        {
            var eventReview = await _repository.Entity.Where(r => r.EventId == EventId && r.VolunteerId == VolunteerId).FirstOrDefaultAsync();

            if (eventReview == null)
            {
                return;
            }

            _repository.Entity.Remove(eventReview);
        }        
    }
}
