using Proj3.Domain.Entities.Volunteer;

namespace Proj3.Application.Common.Interfaces.Persistence.Volunteer
{
    public interface IReviewRepository
    {
        Task<List<Review>> GetReviewsByNgo(Guid ngoId);

        Task<List<Review>> GetReviewsByEvent(Guid eventId);

        Task<float> GetAverageRatingByEvent(Guid eventId);

        Task<Review> AddAsync(Review review);

        Task<bool> UpdateAsync(Review review);

        Task DeleteAsync(Guid EventId, Guid VolunteerId);

        Task<bool> UserAlreadyPostReviewAsync(Guid eventId, Guid volunteerId);
    }
}
