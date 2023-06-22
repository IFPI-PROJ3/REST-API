using Microsoft.AspNetCore.Http;
using Proj3.Contracts.NGO.Response;
using Proj3.Domain.Entities.NGO;

namespace Proj3.Application.Common.Interfaces.Services.NGO.Commands
{
    public interface INgoCommandService
    {
        Task<NgoStatusResponse> UpdateAsync(HttpContext httpContext, Ngo ngo);

        //Task<NgoStatusResponse> AddQuickEvent();
        //Task<NgoStatusResponse> AddEvent();
    }
}
