using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Proj3.Api.Controllers.Volunteers
{
    ///
    [ApiController]    
    [Route("volunteer")]
    [ApiVersion("1.0")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class VolunteerController : ControllerBase
    {
        /// <summary>
        /// Volunteer request to participate in the event
        /// </summary>                
        [HttpGet("event-request/{id}")]
        public IActionResult EventRequest([FromQuery]Guid id)
        {
            //var userVolunteer = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Ok(200);
        }        
    }
}
