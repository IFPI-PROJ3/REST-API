using Microsoft.AspNetCore.Http;
using Proj3.Contracts.Volunteer.Response;

namespace Proj3.Application.Common.Interfaces.Services.Volunteer.Queries
{
    public interface IVolunteerQueryService
    {
        public Task<VolunteerPageInfo> GetVolunteerInitialPageAsync(HttpContext httpContext);

        public Task<VolunteerPageInfo> GetVolunteerPageAsync(HttpContext httpContext, Guid volunteerId);

    }
}
