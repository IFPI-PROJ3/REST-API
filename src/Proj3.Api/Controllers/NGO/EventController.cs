using Microsoft.AspNetCore.Mvc;
using Proj3.Application.Common.Interfaces.Services.Common.Queries;
using Proj3.Application.Common.Interfaces.Services.NGO.Commands;
using Proj3.Application.Common.Interfaces.Services.NGO.Queries;
using Proj3.Application.Services.NGO.Commands;
using Proj3.Contracts.Authentication.Response;
using Proj3.Contracts.NGO.Request;
using Proj3.Domain.Entities.NGO;
using System.Net.Mime;

namespace Proj3.Api.Controllers.NGO
{
    /// <summary>
    /// Event controller
    /// </summary>
    [ApiController]
    [Route("event")]
    [ApiVersion("1.0")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class EventController : ControllerBase
    {
        private readonly IEventCommandService _eventCommandService;
        private readonly IEventQueryService _eventQueryService;

        /// <summary>
        /// Category constructor
        /// </summary>
        public EventController(IEventCommandService eventCommandService, IEventQueryService eventQueryService)
        {
            _eventCommandService = eventCommandService;
            _eventQueryService =  eventQueryService;
        }

        /// <summary>
        /// Get feed events (Volunteer)
        /// </summary>                
        /// <response code="200">Events collection</response>
        /// <response code="401">Invalid credentials</response>        
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        [HttpGet("/feed")]
        public IActionResult GetFeedEventsAsync([FromBody] EventsFeedRequest eventsFeedRequest)
        {
            var events = _eventQueryService.GetEventsFeed(HttpContext, eventsFeedRequest);
            return StatusCode(StatusCodes.Status200OK, events);
        }

        /// <summary>
        /// Get event by id (NGO/Volunteer)
        /// </summary>                
        /// <param name="id">Event id</param>
        /// <response code="200">Event</response>
        /// <response code="401">Invalid credentials</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        [HttpGet("{id}")]
        public IActionResult GetEventById([FromQuery] Guid id)
        {            
            return StatusCode(StatusCodes.Status501NotImplemented);
        }        

        /// <summary>
        /// Get ngo events (NGO/Volunteer)
        /// </summary>                
        /// <param name="id">Ngo id</param>
        /// <response code="501">Not implemented</response>
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        [HttpGet("ngo/{id}")]
        public IActionResult GetNgoEvents([FromQuery] Guid id)
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }

        /// <summary>
        /// Get active ngo events (NGO/Volunteer)
        /// </summary>                
        /// <param name="id">Ngo id</param>
        /// <response code="501">Not implemented</response>
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        [HttpGet("actives/ngo/{id}")]
        public IActionResult GetActiveNgoEvents([FromQuery] Guid id)
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }

        /// <summary>
        /// Create a new event (NGO)
        /// </summary>                
        /// <param name="id">Event id</param>
        /// <response code="501">Not implemented</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("new")]
        public async Task<IActionResult> NewEventAsync(NewEventRequest newEventRequest)
        {            
            var addedEvent = await _eventCommandService.AddAsync(HttpContext, newEventRequest);                        

            return StatusCode(StatusCodes.Status501NotImplemented, addedEvent);
        }

        /// <summary>
        /// Update event
        /// </summary>                
        /// <param name="id">Event id</param>
        /// <param name="updateEventRequest">Event info to update</param>
        /// <response code="501">Not implemented</response>
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> UpdateEventAsync([FromQuery] Guid id, [FromBody] UpdateEventRequest updateEventRequest)
        {
            var updatedEvent = await _eventCommandService.UpdateAsync(HttpContext, updateEventRequest);

            return StatusCode(StatusCodes.Status501NotImplemented, updatedEvent);
        }

        /// <summary>
        /// Cancel event
        /// </summary>                
        /// <param name="id">Event id</param>
        /// <response code="501">Not implemented</response>
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        [HttpPut("cancel/{id}")]
        public IActionResult CancelEvent([FromQuery] Guid id)
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }
    }
}
