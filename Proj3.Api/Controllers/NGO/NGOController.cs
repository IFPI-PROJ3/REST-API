using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proj3.Contracts.NGO.Request;
using System.Security.Claims;

namespace Proj3.Api.Controllers.NGO
{
    [ApiController]
    [Authorize]
    [Route("ngo")]
    [ApiVersion("1.0")]
    public class NGOController : ControllerBase
    {

        /// <summary>
        /// All ngo's
        /// </summary>                
        [HttpGet("all")]
        public async Task<IActionResult> AllNgos([FromQuery] SearchOptionsRequest request)
        {
            //var userVolunteer = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Ok(200);
        }

        /// <summary>
        /// All confirmed volunteers in event
        /// </summary>                
        [HttpGet("event/{id}")]
        public async Task<IActionResult> AllVolunteersFromEvent([FromQuery] SearchOptionsRequest request)
        {
            //var userNGO = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Ok(200);
        }
    }
}
