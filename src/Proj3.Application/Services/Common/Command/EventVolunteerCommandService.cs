using Microsoft.AspNetCore.Http;
using Proj3.Application.Common.Errors.Authentication;
using Proj3.Application.Common.Interfaces.Persistence.Authentication;
using Proj3.Application.Common.Interfaces.Persistence.Common;
using Proj3.Application.Common.Interfaces.Persistence.Volunteer;
using Proj3.Application.Common.Interfaces.Services.Common.Command;
using Proj3.Domain.Entities.Authentication;

namespace Proj3.Application.Services.Common.Command
{
    public class EventVolunteerCommandService : IEventVolunteerCommandService
    {
        private readonly IEventVolunteerRepository _eventVolunteerRepository;
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IUserRepository _userRepository;

        public EventVolunteerCommandService(IEventVolunteerRepository eventVolunteerRepository, IVolunteerRepository volunteerRepository, IUserRepository userRepository)
        {
            _eventVolunteerRepository = eventVolunteerRepository;
            _volunteerRepository = volunteerRepository;
            _userRepository = userRepository;
        }

        public async Task AcceptRequestAsync(HttpContext httpContext, Guid eventVolunteerId)
        {
            Guid userId = Utils.Authentication.User.GetUserIdFromHttpContext(httpContext);

            if (await _userRepository.GetUserByIdAsync(userId) is not User user)
            {
                throw new InvalidCredentialsException();
            }

            if (user.UserRole != UserRole.Ngo)
            {
                throw new NotFoundException();
            }

            await _eventVolunteerRepository.AcceptRequestAsync(eventVolunteerId);
        }

        public async Task RefuseRequest(HttpContext httpContext, Guid eventVolunteerId)
        {
            Guid userId = Utils.Authentication.User.GetUserIdFromHttpContext(httpContext);

            if (await _userRepository.GetUserByIdAsync(userId) is not User user)
            {
                throw new InvalidCredentialsException();
            }

            if (user.UserRole != UserRole.Ngo)
            {
                throw new NotFoundException();
            }

            await _eventVolunteerRepository.RefuseRequestAsync(eventVolunteerId);
        }

        public async Task NewRequestAsync(HttpContext httpContext, Guid eventId)
        {
            Guid userId = Utils.Authentication.User.GetUserIdFromHttpContext(httpContext);

            if (await _userRepository.GetUserByIdAsync(userId) is not User user)
            {
                throw new InvalidCredentialsException();
            }

            if (user.UserRole != UserRole.Volunteer)
            {
                throw new NotFoundException();
            }

            var volunteer = await _volunteerRepository.GetByUserIdAsync(user.Id);
            await _eventVolunteerRepository.NewRequestAsync(eventId, volunteer!.Id);
        }
    }
}
