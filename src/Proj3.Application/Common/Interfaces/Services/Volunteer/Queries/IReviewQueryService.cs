using Proj3.Contracts.Common;

namespace Proj3.Application.Common.Interfaces.Services.Volunteer.Queries
{
    public interface IReviewQueryService
    {
        Task<float> GetAverageRatingByNgoAsync(Guid ngoId);

        Task<List<ReviewToCard>> GetReviewsByNgo(Guid ngoId);
    }
}
