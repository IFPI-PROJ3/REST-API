namespace Proj3.Application.Common.Interfaces.Persistence.Review
{
    public interface IReviewRepository
    {
        Task<Domain.Entities.Common.Review> Add(Domain.Entities.Common.Review review);

        Task<Domain.Entities.Common.Review> Update(Domain.Entities.Common.Review review);

        Task Delete(Guid EventId, Guid VolunteerId);

        Task<float> GetAverageRatingByNgo(Guid ngoId);        
    }
}
