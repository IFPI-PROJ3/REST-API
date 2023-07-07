using Microsoft.AspNetCore.Mvc;
using Proj3.Application.Common.Interfaces.Services.Common.Command;
using Proj3.Application.Common.Interfaces.Services.Common.Queries;
using Proj3.Contracts.NGO.Response;
using System.Net.Mime;

namespace Proj3.Api.Controllers.NGO
{
    /// <summary>
    /// Event-volunteer controller
    /// </summary>
    [ApiController]
    [Route("event-volunteers")]
    [ApiVersion("1.0")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class EventVolunteerController : ControllerBase
    {
        private readonly IEventVolunteerCommandService _eventVolunteerCommandService;
        private readonly IEventVolunteerQueryService _eventVolunteerQueryService;
        
        ///         
        public EventVolunteerController(IEventVolunteerCommandService eventVolunteerCommandService, IEventVolunteerQueryService eventVolunteerQueryService)
        {
            _eventVolunteerCommandService = eventVolunteerCommandService;
            _eventVolunteerQueryService = eventVolunteerQueryService;
        }

        /// <summary>
        /// Get event volunteers requests (NGO)
        /// </summary>     
        /// <param name="id">Event id</param>
        /// <response code="200">Collection with event requests</response>
        /// <response code="401">Not authorized</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(List<EventRequestResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("requests/{id}")]
        public async Task<IActionResult> EventRequestsAsync([FromQuery] Guid id)
        {
            var requests = await _eventVolunteerQueryService.GetRequestsByEvent(HttpContext, id);
            return StatusCode(StatusCodes.Status200OK, requests);
        }

        /// <summary>
        /// NGO accepts volunteer request (NGO)
        /// </summary>     
        /// <param name="id">Event volunteer id</param>
        /// <response code="204">Accepted</response>
        /// <response code="401">Not authorized</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("accept-request/{id}")]
        public async Task<IActionResult> EventAcceptRequestAsync([FromQuery] Guid id)
        {
            await _eventVolunteerCommandService.AcceptRequestAsync(HttpContext, id);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// NGO refuse volunteer request (NGO)
        /// </summary>     
        /// <param name="id">Event volunteer id</param>
        /// <response code="204">Refused</response>
        /// <response code="401">Not authorized</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("refuse-request/{id}")]
        public async Task<IActionResult> EventRefuseRequestAsync([FromQuery] Guid id)
        {
            await _eventVolunteerCommandService.RefuseRequest(HttpContext, id);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// Volunteer requests participation in event (Volunteer)
        /// </summary>     
        /// <param name="id">Event id</param>
        /// <response code="204">Requested</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">NotFound</response>
        /// <response code="500">InternalServerError</response>  
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]        
        [HttpPost("request/{id}")]
        public async Task<IActionResult> EventRequestAsync([FromQuery] Guid id)
        {
            await _eventVolunteerCommandService.NewRequestAsync(HttpContext, id);
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
