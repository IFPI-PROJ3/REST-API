using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Proj3.Api.Controllers.Volunteers
{
    /// <summary>
    /// Review controller
    /// </summary>
    [ApiController]
    [Route("review")]
    [ApiVersion("1.0")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class ReviewController : ControllerBase
    {
        /// <summary>
        /// Get event reviews (NGO/Volunteer)
        /// </summary>                
        /// <param name="id">Event id</param>
        /// <response code="501">Not implemented</response>
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        [HttpGet("event/{id}")]
        public IActionResult GetReviews([FromQuery] Guid id)
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }

        /// <summary>
        /// Create a new review (Volunteer)
        /// </summary>                
        /// <param name="id">Review id</param>
        /// <response code="501">Not implemented</response>
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        [HttpPost("new/{id}")]
        public IActionResult CreateReview([FromQuery] Guid id)
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }

        /// <summary>
        /// Update review (Volunteer)
        /// </summary>                
        /// <param name="id">Review id</param>
        /// <response code="501">Not implemented</response>
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        [HttpPut("edit/{id}")]
        public IActionResult UpdateReview([FromQuery] Guid id)
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }

        /// <summary>
        /// Delete review (Volunteer)
        /// </summary>                
        /// <param name="id">Review id</param>
        /// <response code="501">Not implemented</response>
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        [HttpPut("delete/{id}")]
        public IActionResult DeleteReview([FromQuery] Guid id)
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }
    }
}
