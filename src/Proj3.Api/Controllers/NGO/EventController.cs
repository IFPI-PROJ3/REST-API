using Microsoft.AspNetCore.Mvc;
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
        /// <summary>
        /// Get event by id (NGO/Volunteer)
        /// </summary>                
        /// <param name="id">Event id</param>
        /// <response code="501">Not implemented</response>
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        [HttpGet("{id}")]
        public IActionResult GetEvent([FromQuery] Guid id)
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
        /// Get events by category (NGO/Volunteer)
        /// </summary>                
        /// <param name="id">Category id</param>
        /// <response code="501">Not implemented</response>
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        [HttpGet("category/{id}")]
        public IActionResult GetEventsByCategory([FromQuery] Guid id)
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }

        /// <summary>
        /// Create a new event (NGO)
        /// </summary>                
        /// <param name=""></param>
        /// <response code="501">Not implemented</response>
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        [HttpPost("new")]
        public IActionResult CreateEvent()
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }

        /// <summary>
        /// Update event
        /// </summary>                
        /// <param name=""></param>
        /// <response code="501">Not implemented</response>
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        [HttpPut("edit/{id}")]
        public IActionResult UpdateEvent()
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
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
