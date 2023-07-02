using Microsoft.AspNetCore.Http;
using Proj3.Application.Common.Interfaces.Persistence.NGO;
using Proj3.Application.Common.Interfaces.Persistence.Volunteer;
using Proj3.Application.Common.Interfaces.Services.Volunteer.Queries;
using Proj3.Domain.Entities.NGO;
using Proj3.Domain.Entities.Volunteer;
using System.Collections.Generic;

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
            List<float> allNgoRating = new();
            List<Review> eventReviews = new();

            var ngoEvents = await _eventRepository.GetAllByNgoAsync(ngoId).ToListAsync();

            foreach (Event @event in ngoEvents)
            {
                eventReviews = await _reviewRepository.GetReviewsByEvent(@event.Id).ToListAsync();
            }

            foreach (Review review in eventReviews)
            {
                allNgoRating.Add(review.Stars);
            }

            return allNgoRating.Average();
        }
    }
}
