using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Proj3.Application.Common.Errors.Authentication;
using Proj3.Application.Common.Interfaces.Persistence.Authentication;
using Proj3.Application.Common.Interfaces.Persistence.Common;
using Proj3.Application.Common.Interfaces.Persistence.NGO;
using Proj3.Application.Common.Interfaces.Persistence.Volunteer;
using Proj3.Application.Common.Interfaces.Services.Volunteer.Queries;
using Proj3.Contracts.NGO.Response;
using Proj3.Contracts.Volunteer.Response;
using Proj3.Domain.Entities.NGO;

namespace Proj3.Application.Services.Volunteer.Queries
{
    public class VolunteerQueryService : IVolunteerQueryService
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IEventVolunteerRepository _eventVolunteerRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IEventImagesRepository _eventImagesRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IUserRepository _userRepository;

        public VolunteerQueryService(IVolunteerRepository volunteerRepository, IEventRepository eventRepository, IEventVolunteerRepository eventVolunteerRepository, ICategoryRepository categoryRepository, IEventImagesRepository eventImagesRepository, IReviewRepository reviewRepository, IUserRepository userRepository)
        {
            _volunteerRepository = volunteerRepository;
            _eventRepository = eventRepository;
            _eventVolunteerRepository = eventVolunteerRepository;
            _categoryRepository = categoryRepository;
            _eventImagesRepository = eventImagesRepository;
            _reviewRepository = reviewRepository;
            _userRepository = userRepository;
        }

        public Task<VolunteerPageInfo> GetVolunteerInitialPageAsync(HttpContext httpContext)
        {
            throw new NotImplementedException();
        }

        public async Task<VolunteerPageInfo> GetVolunteerPageAsync(HttpContext httpContext, Guid volunteerId)
        {
            if(await _volunteerRepository.GetByIdAsync(volunteerId) is not Domain.Entities.Volunteer.Volunteer volunteer)
            {
                throw new NotFoundException();
            }
            
            var categories = await _categoryRepository.GetAllCategoriesByVolunteerAsync(volunteerId);

            var upcoming_events = await TransformEventToCard(await _eventRepository.GetUpcomingEventsByVolunteerAsync(volunteerId));
            var active_events = await TransformEventToCard(await _eventRepository.GetActiveEventsByVolunteerAsync(volunteerId));
            var ended_events = await TransformEventToCard(await _eventRepository.GetEndedEventsByVolunteerAsync(volunteerId));
            
            return new VolunteerPageInfo(volunteer!, categories, upcoming_events, active_events, ended_events);
        }

        public async Task<List<EventToCard>> TransformEventToCard(List<Event> events)
        {
            if (events.Count == 0)
            {
                return new List<EventToCard>();
            }

            List<string> eventCategories = await _categoryRepository.GetAllCategoriesByNgoAsync(events.First().Id);
            List<EventToCard> eventsToCard = new List<EventToCard>();

            foreach (Event @event in events)
            {
                EventImage? thumbImage = await _eventImagesRepository.GetThumbImageAsync(@event.Id);
                int requestsCount = await _eventVolunteerRepository.GetEventRequestsCount(@event.Id);
                int volunteersCount = await _eventVolunteerRepository.GetEventVolunteersCount(@event.Id);

                eventsToCard.Add(new EventToCard
                    (
                        @event.Id,
                        @event.NgoId,
                        @event.Title,
                        @event.Description,
                        @event.QuickEvent,
                        @event.VolunteersLimit,
                        requestsCount,
                        volunteersCount,
                        @event.StartDate,
                        @event.EndDate,
                        @event.CreatedAt,
                        @event.UpdatedAt,
                        eventCategories,
                        thumbImage is null ? null : thumbImage.Id.ToString()
                    )
                );
            }

            return eventsToCard;
        }

        public async Task<List<EventToCardVolunteer>> TransformEventToCardVolunteer(List<Event> events, Guid volunteerId)
        {
            if (events.Count == 0)
            {
                return new List<EventToCardVolunteer>();
            }

            List<string> eventCategories = await _categoryRepository.GetAllCategoriesByNgoAsync(events.First().Id);
            List<EventToCardVolunteer> eventsToCard = new();

            foreach (Event @event in events)
            {
                EventImage? thumbImage = await _eventImagesRepository.GetThumbImageAsync(@event.Id);
                int requestsCount = await _eventVolunteerRepository.GetEventRequestsCount(@event.Id);
                int volunteersCount = await _eventVolunteerRepository.GetEventVolunteersCount(@event.Id);

                var canBeReviewed = false;

                if(@event.EndDate < DateTime.Now && !await _reviewRepository.UserAlreadyPostReviewAsync(@event.Id, volunteerId))
                {
                    canBeReviewed = true;
                }

                eventsToCard.Add(new EventToCardVolunteer
                    (
                        @event.Id,
                        @event.NgoId,
                        @event.Title,
                        @event.Description,
                        @event.QuickEvent,
                        @event.VolunteersLimit,
                        requestsCount,
                        volunteersCount,
                        @event.StartDate,
                        @event.EndDate,
                        @event.CreatedAt,
                        @event.UpdatedAt,
                        eventCategories,
                        thumbImage is null ? null : thumbImage.Id.ToString(),
                        canBeReviewed
                    )
                ); 
            }

            return eventsToCard;
        }
    }
}
