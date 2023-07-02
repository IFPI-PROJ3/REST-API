using Proj3.Application.Common.Interfaces.Persistence.Common;
using Proj3.Infrastructure.Services;

namespace Proj3.Infrastructure.Persistence.Repositories.Common
{
    public class UserImageRepository : IUserImageRepository
    {
        public UserImageRepository() { }

        public async Task AddUserImageAsync(byte[] image, Guid userId)
        {
            try
            {
                await S3Service.FileUploadAsync(image, userId.ToString());                
            }
            catch (Exception)
            {
                await S3Service.DeleteFileAsync(userId.ToString());
                throw;
            }
        }

        public async Task DeleteUserImageAsync(Guid userId)
        {
            await S3Service.DeleteFileAsync(userId.ToString());
        }
    }
}
