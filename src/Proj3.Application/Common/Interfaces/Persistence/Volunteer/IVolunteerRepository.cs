namespace Proj3.Application.Common.Interfaces.Persistence.Volunteer
{
    public interface IVolunteerRepository
    {
        Task AddAsync(Domain.Entities.Volunteer.Volunteer volunteer);

        Task UpdateAsync(Domain.Entities.Volunteer.Volunteer volunteer);

        Task<Domain.Entities.Volunteer.Volunteer?> GetByIdAsync(Guid volunteerId);

        Task<Domain.Entities.Volunteer.Volunteer?> GetByUserIdAsync(Guid userId);        
    }
}
