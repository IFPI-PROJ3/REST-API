using Proj3.Application.Common.Interfaces.Persistence.Volunteer;
using Proj3.Application.Common.Interfaces.Services.Volunteer.Commands;
using Proj3.Domain.Entities.Volunteer;

namespace Proj3.Application.Services.Volunteer.Commands
{
    public class ReviewCommandService : IReviewCommandService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewCommandService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<Review> AddAsync(Review review)
        {
            return await _reviewRepository.AddAsync(review);
        }

        public async Task<bool> UpdateAsync(Review review)
        {
            return await _reviewRepository.UpdateAsync(review);
        }

        public async Task DeleteAsync(Guid EventId, Guid VolunteerId)
        {
            await _reviewRepository.DeleteAsync(EventId, VolunteerId);
        }
    }
}
