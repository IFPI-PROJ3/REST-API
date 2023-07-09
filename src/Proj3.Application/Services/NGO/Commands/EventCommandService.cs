using Microsoft.AspNetCore.Http;
using Proj3.Application.Common.Errors.Authentication;
using Proj3.Application.Common.Interfaces.Others;
using Proj3.Application.Common.Interfaces.Persistence.Authentication;
using Proj3.Application.Common.Interfaces.Persistence.NGO;
using Proj3.Application.Common.Interfaces.Services.NGO.Commands;
using Proj3.Contracts.NGO.Request;
using Proj3.Contracts.NGO.Response;
using Proj3.Domain.Entities.Authentication;
using Proj3.Domain.Entities.NGO;
using System.Diagnostics;
using System.Transactions;

namespace Proj3.Application.Services.NGO.Commands
{
    public class EventCommandService :  IEventCommandService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IEventImagesRepository _eventImagesRepository;
        private readonly INgoRepository _ngoRepository;        
        private readonly IUserRepository _userRepository;
        private readonly ITransactionsManager _transactionsManager;

        public EventCommandService(IEventRepository eventRepository, IEventImagesRepository eventImagesRepository, INgoRepository ngoRepository, IUserRepository userRepository, ITransactionsManager transactionsManager) 
        { 
            _eventRepository = eventRepository;
            _eventImagesRepository = eventImagesRepository;
            _ngoRepository = ngoRepository;
            _userRepository = userRepository;
            _transactionsManager = transactionsManager;
        }

        public async Task<NewEventResponse> AddAsync(HttpContext httpContext, NewEventRequest newEventRequest)
        {
            Guid userId = Utils.Authentication.User.GetUserIdFromHttpContext(httpContext);
            
            if(await _userRepository.GetUserByIdAsync(userId) is not User user)
            {
                throw new InvalidCredentialsException();
            }

            if(user.UserRole != UserRole.Ngo)
            {
                throw new NotFoundException();
            }

            if(await _ngoRepository.GetByUserId(userId) is not Ngo ngo)
            {
                throw new NotFoundException();
            }

            try
            {
                await _transactionsManager.BeginTransactionAsync();

                Event @event = Event.NewEvent(ngo.Id, newEventRequest.title, newEventRequest.description, newEventRequest.volunteers_limit, newEventRequest.start_date, newEventRequest.end_date);                
                @event = await _eventRepository.AddAsync(@event);
                 
                EventImage eventImage = EventImage.NewThumbImage(@event.Id);
                eventImage = await _eventImagesRepository.AddOrUpdateThumbAsync(newEventRequest.image_thumb, eventImage);

                await _transactionsManager.CommitTransactionAsync();

                return new NewEventResponse(@event);
            }
            catch(Exception)
            {
                await _transactionsManager.RollbackTransactionAsync();
                throw;
            }                                    
        }

        public async Task<UpdatedEventResponse> UpdateAsync(HttpContext httpContext, Guid eventId, UpdateEventRequest updateEventRequest)
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

            if (await _ngoRepository.GetByUserId(userId) is not Ngo ngo)
            {
                throw new NotFoundException();
            }

            if (await _eventRepository.GetEventByIdAsync(eventId) is not Event @event)
            {
                throw new NotFoundException();
            }

            try
            {
                await _transactionsManager.BeginTransactionAsync();

                @event.Title = updateEventRequest.title;
                @event.Description = updateEventRequest.description;
                                
                await _eventRepository.UpdateAsync(@event);                

                if (updateEventRequest.image_thumb is not null)
                {                                        
                    var eventImage = (await _eventImagesRepository.GetEventImagesAsync(@eventId))[0];
                    eventImage = await _eventImagesRepository.AddOrUpdateThumbAsync(updateEventRequest.image_thumb, eventImage);
                }

                await _transactionsManager.CommitTransactionAsync();

                return new UpdatedEventResponse(@event);

            } 
            catch (Exception)
            {
                await _transactionsManager.RollbackTransactionAsync();
                throw;
            }            
        }

        public async Task CancelAsync(HttpContext httpContext, Guid eventId)
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

            if (await _ngoRepository.GetByUserId(userId) is not Ngo ngo)
            {
                throw new NotFoundException();
            }

            if (await _eventRepository.GetEventByIdAsync(eventId) is not Event @event)
            {
                throw new NotFoundException();
            }

            await _eventRepository.CancelEventAsync(eventId);
        }
    }
}
