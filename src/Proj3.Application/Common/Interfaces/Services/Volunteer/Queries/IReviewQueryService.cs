using Microsoft.AspNetCore.Http;

namespace Proj3.Application.Common.Interfaces.Services.Volunteer.Queries
{
    public interface IReviewQueryService
    {
        Task<float> GetAverageRatingByNgoAsync(Guid ngoId);
    }
}
