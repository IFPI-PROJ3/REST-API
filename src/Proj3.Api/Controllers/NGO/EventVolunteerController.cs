using Microsoft.AspNetCore.Mvc;
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
        /// <summary>
        /// Get event volunteers requests (NGO)
        /// </summary>     
        /// <param name="id">Event id</param>
        /// <response code="501">Not implemented</response>
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        [HttpGet("requests/{id}")]
        public IActionResult EventRequests([FromQuery] Guid id)
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }

        /// <summary>
        /// NGO accepts volunteer request (NGO)
        /// </summary>     
        /// <param name="id">Event volunteer id</param>
        /// <response code="501">Not implemented</response>
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        [HttpPut("accept-request/{id}")]
        public IActionResult EventAcceptRequest([FromQuery] Guid id)
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }

        /// <summary>
        /// NGO refuse volunteer request (NGO)
        /// </summary>     
        /// <param name="id">Event volunteer id</param>
        /// <response code="501">Not implemented</response>
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        [HttpPut("refuse-request/{id}")]
        public IActionResult EventRefuseRequest([FromQuery] Guid id)
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }

        /// <summary>
        /// Volunteer requests participation in event (Volunteer)
        /// </summary>     
        /// <param name="id">Event id</param>
        /// <response code="501">Not implemented</response>
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        [HttpPost("request/{id}")]
        public IActionResult EventRequest([FromQuery] Guid id)
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }
    }
}
