﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Proj3.Api.Controllers.Volunteers
{
    ///
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
        public IActionResult EventRequest()
        {
            //var userVolunteer = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Ok(200);
        }        
    }
}