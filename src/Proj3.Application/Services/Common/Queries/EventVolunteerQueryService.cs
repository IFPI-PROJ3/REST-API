using Microsoft.AspNetCore.Http;
using Proj3.Application.Common.Errors.Authentication;
using Proj3.Application.Common.Interfaces.Persistence.Authentication;
using Proj3.Application.Common.Interfaces.Persistence.Common;
using Proj3.Application.Common.Interfaces.Persistence.Volunteer;
using Proj3.Application.Common.Interfaces.Services.Common.Queries;
using Proj3.Contracts.NGO.Response;
using Proj3.Domain.Entities.Authentication;

namespace Proj3.Application.Services.Common.Queries
{
    public class EventVolunteerQueryService : IEventVolunteerQueryService
    {
        private readonly IEventVolunteerRepository _eventVolunteerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IVolunteerRepository _volunteerRepository;
        public EventVolunteerQueryService(IEventVolunteerRepository eventVolunteerRepository, IUserRepository userRepository, IVolunteerRepository volunteerRepository)
        {
            _eventVolunteerRepository = eventVolunteerRepository;
            _userRepository = userRepository;
            _volunteerRepository = volunteerRepository;
        }

        public async Task<List<EventRequestResponse>> GetEventVolunteersByEvent(HttpContext httpContext, Guid eventId)
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

            var eventsRequest = await _eventVolunteerRepository.GetEventVolunteersByEvent(eventId);
            var eventRequestResponse = new List<EventRequestResponse>();

            foreach (var request in eventsRequest)
            {
                var volunteer = await _volunteerRepository.GetByIdAsync(request.VolunteerId);
                var volunteerUser = await _userRepository.GetUserByIdAsync(volunteer!.UserId);
                eventRequestResponse.Add(new EventRequestResponse(request.Id, request.EventId, request.VolunteerId, volunteer.Name + " " + volunteer.LastName, volunteerUser!.Email));
            }

            return eventRequestResponse;
        }

        public async Task<List<EventRequestResponse>> GetRequestsByEvent(HttpContext httpContext, Guid eventId)
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

            var eventsRequest = await _eventVolunteerRepository.GetRequestsByEvent(eventId);
            var eventRequestResponse = new List<EventRequestResponse>();            

            foreach (var request in eventsRequest)
            {
                var volunteer = await _volunteerRepository.GetByIdAsync(request.VolunteerId);
                var volunteerUser = await _userRepository.GetUserByIdAsync(volunteer!.UserId);
                eventRequestResponse.Add(new EventRequestResponse(request.Id, request.EventId, request.VolunteerId, volunteer.Name + " " + volunteer.LastName, volunteerUser!.Email));
            }

            return eventRequestResponse;            
        }
    }
}
