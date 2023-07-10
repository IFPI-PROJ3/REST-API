using Proj3.Application.Common.Interfaces.Persistence.Authentication;
using Proj3.Application.Common.Interfaces.Persistence.NGO;
using Proj3.Application.Common.Interfaces.Persistence.Volunteer;
using Proj3.Application.Common.Interfaces.Services.Volunteer.Queries;
using Proj3.Contracts.Common;
using Proj3.Domain.Entities.NGO;
using Proj3.Domain.Entities.Volunteer;

namespace Proj3.Application.Services.Volunteer.Queries
{
    public class ReviewQueryService : IReviewQueryService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IUserRepository _userRepository;

        public ReviewQueryService(IReviewRepository reviewRepository, IEventRepository eventRepository, IVolunteerRepository volunteerRepository, IUserRepository userRepository)
        {
            _reviewRepository = reviewRepository;
            _eventRepository = eventRepository;
            _volunteerRepository = volunteerRepository;
            _userRepository = userRepository;
        }

        public async Task<float> GetAverageRatingByNgoAsync(Guid ngoId)
        {
            List<float> allNgoRating = new();
            List<Review> eventReviews = new();

            var ngoEvents = await _eventRepository.GetAllByNgoAsync(ngoId).ToListAsync();

            foreach (Event @event in ngoEvents)
            {
                eventReviews = await _reviewRepository.GetReviewsByEvent(@event.Id);
            }

            foreach (Review review in eventReviews)
            {
                allNgoRating.Add(review.Stars);
            }

            if (allNgoRating.Count == 0) 
            {
                return -1;
            }

            return allNgoRating.Average();
        }

        public async Task<List<ReviewToCard>> GetReviewsByNgo(Guid ngoId)
        {
            return await ReviewsToCards(await _reviewRepository.GetReviewsByNgo(ngoId));
        }

        public async Task<List<ReviewToCard>> ReviewsToCards(List<Review> reviews)
        {
            List<ReviewToCard> reviewsToCards = new();

            if(reviews.Count == 0)
            {
                return new List<ReviewToCard>();
            }


            foreach (Review review in reviews)
            {
                var volunteer = await _volunteerRepository.GetByIdAsync(review.VolunteerId);
                var user = await _userRepository.GetUserByIdAsync(volunteer!.UserId);              

                reviewsToCards.Add(
                    new ReviewToCard(
                        review.EventId,
                        review.VolunteerId,
                        volunteer.Name + " " + volunteer.LastName,
                        user!.Id.ToString(),
                        review.Content,
                        review.Stars,
                        review.CreatedAt
                    )
                );
            }

            return reviewsToCards;
        }
    }
}
