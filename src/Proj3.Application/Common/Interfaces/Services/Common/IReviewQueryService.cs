namespace Proj3.Application.Common.Interfaces.Services.Common
{
    public interface IReviewQueryService
    {
        Task<float> GetAverageRatingByNgo(Guid ngoId);
    }
}
