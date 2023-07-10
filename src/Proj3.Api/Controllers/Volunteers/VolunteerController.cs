using Microsoft.AspNetCore.Mvc;
using Proj3.Application.Common.Interfaces.Services.NGO.Commands;
using Proj3.Application.Common.Interfaces.Services.Volunteer.Queries;
using Proj3.Contracts.NGO.Response;
using Proj3.Domain.Entities.NGO;
using System.Net.Mime;

namespace Proj3.Api.Controllers.Volunteers
{
    /// <summary>
    /// Volunteer controller
    /// </summary>
    [ApiController]
    [Route("volunteer")]
    [ApiVersion("1.0")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class VolunteerController : ControllerBase
    {
        private readonly IVolunteerQueryService _volunteerQueryService;        

        ///
        public VolunteerController(IVolunteerQueryService volunteerQueryService)
        {
            _volunteerQueryService = volunteerQueryService;
        }

        /// <summary>
        /// Volunteer initial page (Volunteer View)
        /// </summary>                     
        /// <response code="200">Volunteer information</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">NotFound</response>
        /// <response code="500">InternalServerError</response>  
        //[ProducesResponseType(typeof(NgoPageInfo), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("initial-page")]
        public async Task<ActionResult> InitialPageAsync()
        {
            //List<string> categories = await _categoryQueryService(ngo.Id);
            //float average_rating = await _reviewQueryService.GetAverageRatingByNgoAsync(ngo.Id);

            //List<EventToCard> upcomingEvents = await _eventQueryService.GetUpcomingEventsByNgoAsync(HttpContext, ngo.Id);
            //List<EventToCard> activeEvents = await _eventQueryService.GetActiveEventsByNgoAsync(HttpContext, ngo.Id);
            //List<EventToCard> endedEvents = await _eventQueryService.GetEndedEventsByNgoAsync(HttpContext, ngo.Id);

            //NgoPageInfo ngoPageInfo = new NgoPageInfo(ngo, categories, average_rating, upcomingEvents, activeEvents, endedEvents);

            return StatusCode(StatusCodes.Status501NotImplemented);
        }

        /// <summary>
        /// Volunteer view (NGO and Volunteer View)
        /// </summary>                     
        /// <response code="200">Volunteer information</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">NotFound</response>
        /// <response code="500">InternalServerError</response>  
        [ProducesResponseType(typeof(NgoPageInfo), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}")]
        public async Task<ActionResult> VolunteerPageAsync(Guid id)
        {            
            var volunterInfo = await _volunteerQueryService.GetVolunteerPageAsync(HttpContext, id);
            return StatusCode(StatusCodes.Status200OK, volunterInfo);
        }
    }
}
