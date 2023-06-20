using Microsoft.EntityFrameworkCore;
using Proj3.Application.Common.Interfaces.Persistence;
using Proj3.Application.Common.Interfaces.Persistence.Volunteer;
using Proj3.Domain.Entities.Volunteer;

namespace Proj3.Infrastructure.Persistence.Repositories.Volunteer
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly IRepositoryBase<Review> _repository;        

        public ReviewRepository(IRepositoryBase<Review> repository)
        {
            _repository = repository;
        }

        IAsyncEnumerable<Review> IReviewRepository.GetReviewsByEvent(Guid eventId)
        {
            return _repository.Entity.Where(r => r.EventId == eventId).AsAsyncEnumerable();
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
