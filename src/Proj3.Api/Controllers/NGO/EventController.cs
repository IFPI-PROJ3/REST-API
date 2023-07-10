using Microsoft.AspNetCore.Mvc;
using Proj3.Application.Common.Interfaces.Services.NGO.Commands;
using Proj3.Application.Common.Interfaces.Services.NGO.Queries;
using Proj3.Contracts.NGO.Request;
using Proj3.Contracts.NGO.Response;
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
        /// <param name="eventsFeedRequest">Event feed filter</param>
        /// <response code="200">Events collection</response>
        /// <response code="401">Unauthorized</response>        
        /// <response code="500">InternalServerError</response>  
        [ProducesResponseType(typeof(EventToCard), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        [Consumes(MediaTypeNames.Application.Json)]
        [HttpPost("feed")]
        public async Task<IActionResult> GetFeedEventsAsync([FromBody] EventsFeedRequest eventsFeedRequest)
        {
            var events = await _eventQueryService.GetEventsFeedAsync(HttpContext, eventsFeedRequest);
            return StatusCode(StatusCodes.Status200OK, events);
        }

        /// <summary>
        /// Get event by id (NGO/Volunteer)
        /// </summary>                
        /// <param name="id">Event id</param>
        /// <response code="200">Event</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">NotFound</response>
        /// <response code="500">InternalServerError</response>  
        [ProducesResponseType(typeof(EventToPage), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById(Guid id)
        {            
            var eventToPage = await _eventQueryService.GetEventByIdAsync(HttpContext, id);
            return StatusCode(StatusCodes.Status200OK, eventToPage);
        }

        /// <summary>
        /// Get ngo events (NGO/Volunteer)
        /// </summary>                
        /// <param name="id">Ngo id</param>
        /// <response code="200">Event</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">NotFound</response>
        /// <response code="500">InternalServerError</response>  
        [ProducesResponseType(typeof(EventToCard), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]        
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("ngo/{id}")]
        public IActionResult GetNgoEvents(Guid id)
        {
            var events = _eventQueryService.GetAllByNgoAsync(HttpContext, id);
            return StatusCode(StatusCodes.Status200OK, events);
        }

        /// <summary>
        /// Create a new event (NGO)
        /// </summary>
        /// <param name="newEventRequest">Event info</param>
        /// <response code="201">Created</response>
        /// <response code="401">Unauthorized</response>        
        /// <response code="500">InternalServerError</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]        
        [HttpPost("new")]
        public async Task<IActionResult> NewEventAsync([FromBody] NewEventRequest newEventRequest)
        {
            var addedEvent = await _eventCommandService.AddAsync(HttpContext, newEventRequest);                        
            return StatusCode(StatusCodes.Status201Created, addedEvent);
        }

        /// <summary>
        /// Update event
        /// </summary>                
        /// <param name="id">Event id</param>
        /// <param name="updateEventRequest">Event info to update</param>
        /// <response code="200">Updated</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">NotFound</response>
        /// <response code="500">InternalServerError</response>      
        [ProducesResponseType(typeof(UpdatedEventResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> UpdateEventAsync(Guid id, [FromBody] UpdateEventRequest updateEventRequest)
        {
            var updatedEvent = await _eventCommandService.UpdateAsync(HttpContext, id, updateEventRequest);
            return StatusCode(StatusCodes.Status200OK, updatedEvent);
        }

        /// <summary>
        /// Cancel event
        /// </summary>                
        /// <param name="id">Event id</param>
        /// <response code="204">NoContent</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">NotFound</response>
        /// <response code="500">InternalServerError</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("cancel/{id}")]
        public async Task<IActionResult> CancelEventAsync(Guid id)
        {
            await _eventCommandService.CancelAsync(HttpContext, id);
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
