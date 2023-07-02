namespace Proj3.Application.Common.Interfaces.Persistence.Common
{
    public interface IUserImageRepository
    {
        public Task AddUserImageAsync(byte[] image, Guid userId);        

        public Task DeleteUserImageAsync(Guid userId);
    }
}
