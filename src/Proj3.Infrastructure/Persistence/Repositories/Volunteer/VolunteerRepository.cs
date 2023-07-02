using Proj3.Application.Common.Interfaces.Persistence.Volunteer;

namespace Proj3.Infrastructure.Persistence.Repositories.Volunteer
{
    public class VolunteerRepository : IVolunteerRepository
    {
        public Task AddAsync(Domain.Entities.Volunteer.Volunteer volunteer)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Domain.Entities.Volunteer.Volunteer volunteer)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Entities.Volunteer.Volunteer?> GetByIdAsync(Guid volunteerId)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Entities.Volunteer.Volunteer?> GetByUserId(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
