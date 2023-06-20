using Microsoft.AspNetCore.Http;
using Proj3.Application.Common.Interfaces.Persistence.NGO;
using Proj3.Application.Common.Interfaces.Persistence.Volunteer;
using Proj3.Application.Common.Interfaces.Services.Volunteer.Queries;
using Proj3.Domain.Entities.NGO;
using Proj3.Domain.Entities.Volunteer;

namespace Proj3.Application.Services.Volunteer.Queries
{
    public class ReviewQueryService : IReviewQueryService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IEventRepository _eventRepository;
        
        public ReviewQueryService(IReviewRepository reviewRepository, IEventRepository eventRepository)
        {
            _reviewRepository = reviewRepository;
            _eventRepository = eventRepository;
        }

        public async Task<float> GetAverageRatingByNgoAsync(Guid ngoId)
        {
            float[] allNgoRating = Array.Empty<float>();
            IAsyncEnumerable<Review> eventReviews = AsyncEnumerable.Empty<Review>();

            var ngoEvents = _eventRepository.GetAllByNgo(ngoId);

            await foreach (Event @event in ngoEvents)
            {
                eventReviews = _reviewRepository.GetReviewsByEvent(@event.Id);
            }

            await foreach (Review review in eventReviews)
            {
                _ = allNgoRating.Append(review.Stars);
            }

            return allNgoRating.Average();
        }
    }
}
