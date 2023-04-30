using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proj3.Contracts.NGO.Request;
using System.Security.Claims;

namespace Proj3.Api.Controllers.Volunteers
{
    [ApiController]
    [Authorize]
    [Route("volunteer")]
    [ApiVersion("1.0")]
    public class VolunteerController : ControllerBase
    {
        /// <summary>
        /// Volunteer request to participate in the event
        /// </summary>                
        [HttpGet("event-request/{id}")]
        public async Task<IActionResult> EventRequest()
        {
            //var userVolunteer = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Ok(200);
        }        
    }
}
