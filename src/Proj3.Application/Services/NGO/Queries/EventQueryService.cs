﻿using Microsoft.AspNetCore.Http;
using Proj3.Application.Common.Errors.Authentication;
using Proj3.Application.Common.Interfaces.Persistence.Authentication;
using Proj3.Application.Common.Interfaces.Persistence.Common;
using Proj3.Application.Common.Interfaces.Persistence.NGO;
using Proj3.Application.Common.Interfaces.Persistence.Volunteer;
using Proj3.Application.Common.Interfaces.Services.NGO.Queries;
using Proj3.Contracts.NGO.Request;
using Proj3.Contracts.NGO.Response;
using Proj3.Domain.Entities.Authentication;
using Proj3.Domain.Entities.NGO;
using Proj3.Domain.Entities.Volunteer;

namespace Proj3.Application.Services.NGO.Queries
{
    public class EventQueryService : IEventQueryService
    {
        private readonly IEventRepository _eventRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IEventImagesRepository _eventImagesRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IUserRepository _userRepository;

        public EventQueryService(IEventRepository eventRepository, ICategoryRepository categoryRepository, IEventImagesRepository eventImagesRepository, IReviewRepository reviewRepository, IUserRepository userRepository)
        {
            _eventRepository = eventRepository;
            _categoryRepository = categoryRepository;
            _eventImagesRepository = eventImagesRepository;
            _reviewRepository = reviewRepository;
            _userRepository = userRepository;
        }

        public async Task<List<EventToCard>> GetEventsFeedAsync(HttpContext httpContext, EventsFeedRequest eventsFeedRequest)
        {
            Guid userId = Utils.Authentication.User.GetUserIdFromHttpContext(httpContext);

            if (await _userRepository.GetUserByIdAsync(userId) is not User user || user.UserRole != UserRole.Volunteer)
            {
                throw new InvalidCredentialsException();
            }
            
            var events = await _eventRepository.GetAllFeedAsync(user.Id, 501, eventsFeedRequest.categories);
            return await TransformEventToCard(events);            
        }        

        public async Task<List<EventToCard>> GetAllByNgoAsync(HttpContext httpContext, Guid ngoId)
        {
            Guid userId = Utils.Authentication.User.GetUserIdFromHttpContext(httpContext);

            if (await _userRepository.GetUserByIdAsync(userId) is not User)
            {
                throw new InvalidCredentialsException();
            }
            
            if(await _eventRepository.GetAllByNgoAsync(ngoId).ToListAsync() is not List<Event> events || events.Count == 0)
            {
                throw new NotFoundException();
            }

            return await TransformEventToCard(events);
        }

        public async Task<EventToPage> GetEventByIdAsync(HttpContext httpContext, Guid eventId)
        {
            Guid userId = Utils.Authentication.User.GetUserIdFromHttpContext(httpContext);

            if (await _userRepository.GetUserByIdAsync(userId) is not User)
            {
                throw new InvalidCredentialsException();
            }

            if (await _eventRepository.GetEventByIdAsync(eventId) is not Event @event)
            {
                throw new NotFoundException();
            }

            List<string> eventCategories = await _categoryRepository.GetAllCategoriesByNgoAsync(@event.NgoId);
            List<Review> eventReviews = (List<Review>)(await _reviewRepository.GetReviewsByEvent(eventId).ToListAsync()).Take(5);
            EventImage? thumbImage = await _eventImagesRepository.GetThumbImageAsync(eventId);

            return new EventToPage
            (
                @event.Id,
                @event.NgoId,
                @event.Title,
                @event.Description,
                @event.QuickEvent,
                @event.VolunteersLimit,
                501,
                @event.StartDate,
                @event.EndDate,
                @event.CreatedAt,
                @event.UpdatedAt,
                eventCategories,
                eventReviews,
                thumbImage is null ? null : thumbImage.Id.ToString()
            );
        }

        public async Task<List<EventToCard>> GetUpcomingEventsByNgoAsync(HttpContext httpContext, Guid ngoId)
        {
            Guid userId = Utils.Authentication.User.GetUserIdFromHttpContext(httpContext);

            if (await _userRepository.GetUserByIdAsync(userId) is not User)
            {
                throw new InvalidCredentialsException();
            }

            if (await _eventRepository.GetUpcomingEventsByNgoAsync(ngoId).ToListAsync() is not List<Event> events || events.Count == 0)
            {
                throw new NotFoundException();
            }

            return await TransformEventToCard(events);
        }

        public async Task<List<EventToCard>> GetActiveEventsByNgoAsync(HttpContext httpContext, Guid ngoId)
        {
            Guid userId = Utils.Authentication.User.GetUserIdFromHttpContext(httpContext);

            if (await _userRepository.GetUserByIdAsync(userId) is not User)
            {
                throw new InvalidCredentialsException();
            }

            if (await _eventRepository.GetActiveEventsByNgoAsync(ngoId).ToListAsync() is not List<Event> events || events.Count == 0)
            {
                throw new NotFoundException();
            }

            return await TransformEventToCard(events);
        }

        public async Task<List<EventToCard>> GetEndedEventsByNgoAsync(HttpContext httpContext, Guid ngoId)
        {
            Guid userId = Utils.Authentication.User.GetUserIdFromHttpContext(httpContext);

            if (await _userRepository.GetUserByIdAsync(userId) is not User)
            {
                throw new InvalidCredentialsException();
            }

            if (await _eventRepository.GetEndedEventsByNgoAsync(ngoId).ToListAsync() is not List<Event> events || events.Count == 0)
            {
                throw new NotFoundException();
            }

            return await TransformEventToCard(events);
        }

        public async Task<List<EventToCard>> TransformEventToCard(List<Event> events)
        {
            List<string> eventCategories = await _categoryRepository.GetAllCategoriesByNgoAsync(events.First().Id);
            List<EventToCard> eventsToCard = new List<EventToCard>();

            foreach (Event @event in events)
            {
                EventImage? thumbImage = await _eventImagesRepository.GetThumbImageAsync(@event.Id);

                eventsToCard.Add(new EventToCard
                    (
                        @event.Id,
                        @event.NgoId,
                        @event.Title,
                        @event.Description,
                        @event.QuickEvent,
                        @event.VolunteersLimit,
                        501,
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
    }
}
