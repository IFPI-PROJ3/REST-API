using Proj3.Domain.Entities.Volunteer;

namespace Proj3.Application.Common.Interfaces.Services.Volunteer.Commands
{
    public interface IReviewCommandService
    {
        Task<Review> AddAsync(Review review);

        Task<bool> UpdateAsync(Review review);

        Task DeleteAsync(Guid EventId, Guid VolunteerId);
    }
}
