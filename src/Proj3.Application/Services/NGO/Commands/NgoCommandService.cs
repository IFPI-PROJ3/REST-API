using Microsoft.AspNetCore.Http;
using Proj3.Application.Common.Errors.Authentication;
using Proj3.Application.Common.Interfaces.Persistence.NGO;
using Proj3.Application.Common.Interfaces.Services.NGO.Commands;
using Proj3.Contracts.NGO.Response;
using Proj3.Domain.Entities.NGO;

namespace Proj3.Application.Services.NGO.Commands
{
    public class NgoCommandService : INgoCommandService
    {
        private readonly INgoRepository _ngoRepository;

        public NgoCommandService(INgoRepository ngoRepository)
        {
            _ngoRepository = ngoRepository;
        }

        public async Task<NgoStatusResponse> UpdateAsync(HttpContext httpContext, Ngo ngo)
        {
            Guid userId = Utils.Authentication.User.GetUserIdFromHttpContext(httpContext);

            if(ngo.UserId != userId)
            {
                throw new NotFoundException();
            }

            await _ngoRepository.UpdateAsync(ngo);

            return new NgoStatusResponse(ngo);
        }
    }
}
