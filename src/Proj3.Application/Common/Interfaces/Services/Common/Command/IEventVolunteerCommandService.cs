using Microsoft.AspNetCore.Http;

namespace Proj3.Application.Common.Interfaces.Services.Common.Command
{
    public interface IEventVolunteerCommandService
    {
        Task AcceptRequestAsync(HttpContext httpContext, Guid eventVolunteerId);

        Task RefuseRequest(HttpContext httpContext, Guid eventVolunteerId);

        Task NewRequestAsync(HttpContext httpContext, Guid eventId);

        Task CancelRequestAsync(HttpContext httpContext, Guid eventId);
    }
}
