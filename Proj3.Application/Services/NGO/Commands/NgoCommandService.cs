﻿using Microsoft.AspNetCore.Http;
using Proj3.Application.Common.Errors.Authentication;
using Proj3.Application.Common.Errors.NGO;
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

        public async Task<NgoStatusResponse> Add(HttpContext httpContext, Ngo ngo)
        {
            Guid userId = Utils.Authentication.User.GetUserIdFromHttpContext(httpContext);

            if (await _ngoRepository.UserNgoAlreadyExists(userId))
            {
                throw new NgoAlreadyExistsException();
            }

            ngo.UserId = userId;
            await _ngoRepository.Add(ngo);

            return new NgoStatusResponse(ngo);
        }

        public async Task<NgoStatusResponse> Update(HttpContext httpContext, Ngo ngo)
        {
            Guid userId = Utils.Authentication.User.GetUserIdFromHttpContext(httpContext);

            if(ngo.UserId != userId)
            {
                throw new NotFoundException();
            }

            await _ngoRepository.Update(ngo);

            return new NgoStatusResponse(ngo);
        }
    }
}
