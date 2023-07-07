using Microsoft.EntityFrameworkCore;
using Proj3.Application.Common.Interfaces.Persistence;
using Proj3.Application.Common.Interfaces.Persistence.Volunteer;

namespace Proj3.Infrastructure.Persistence.Repositories.Volunteer
{
    public class VolunteerRepository : IVolunteerRepository
    {
        private readonly IRepositoryBase<Domain.Entities.Volunteer.Volunteer> _repository;
        
        public VolunteerRepository(IRepositoryBase<Domain.Entities.Volunteer.Volunteer> repository) 
        {
            _repository = repository;
        }

        public async Task AddAsync(Domain.Entities.Volunteer.Volunteer volunteer)
        {
            await _repository.AddAsync(volunteer);
        }

        public async Task UpdateAsync(Domain.Entities.Volunteer.Volunteer volunteer)
        {
            await _repository.UpdateAsync(volunteer);
        }

        public async Task<Domain.Entities.Volunteer.Volunteer?> GetByIdAsync(Guid volunteerId)
        {
            return await _repository.GetByIdAsync(volunteerId);
        }

        public async Task<Domain.Entities.Volunteer.Volunteer?> GetByUserIdAsync(Guid userId)
        {
            return await _repository.Entity.Where(v => v.UserId == userId).FirstOrDefaultAsync();
        }
    }
}
