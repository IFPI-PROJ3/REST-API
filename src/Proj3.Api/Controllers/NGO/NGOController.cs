using Microsoft.AspNetCore.Mvc;
using Proj3.Application.Common.Interfaces.Services.Common.Queries;
using Proj3.Application.Common.Interfaces.Services.NGO.Commands;
using Proj3.Application.Common.Interfaces.Services.NGO.Queries;
using Proj3.Application.Common.Interfaces.Services.Volunteer.Queries;
using Proj3.Application.Services.NGO.Queries;
using Proj3.Contracts.Authentication.Response;
using Proj3.Contracts.NGO.Response;
using Proj3.Domain.Entities.NGO;
using System.Net.Mime;

namespace Proj3.Api.Controllers.NGO
{
    /// <summary>
    /// Ngo controller
    /// </summary>
    [ApiController]    
    [Route("ngo")]
    [ApiVersion("1.0")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]

    public class NGOController : ControllerBase
    {        
        private readonly INgoCommandService _ngoCommandService;
        private readonly INgoQueryService _ngoQueryService;
        private readonly IEventQueryService _eventQueryService;
        private readonly ICategoryQueryService _categoryQueryService;
        private readonly IReviewQueryService _reviewQueryService;

        ///
        public NGOController(INgoCommandService ngoCommandService , INgoQueryService ngoQueryService, IEventQueryService eventQueryService, ICategoryQueryService categoryQueryService, IReviewQueryService reviewQueryService)
        {
            _ngoCommandService = ngoCommandService;            
            _ngoQueryService = ngoQueryService;
            _eventQueryService = eventQueryService;
            _categoryQueryService = categoryQueryService;
            _reviewQueryService = reviewQueryService;
        }

        /// <summary>
        /// NGO page (NGO View)    
        /// </summary>                     
        /// <response code="200">User authenticated</response>
        /// <response code="401">Invalid credentials</response>        
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(AuthenticationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]        
        [HttpGet("initial-page")]
        public async Task<ActionResult> InitialPageAsync()
        {
            Guid userId = Application.Utils.Authentication.User.GetUserIdFromHttpContext(HttpContext);            

            if (await _ngoQueryService.GetByUserIdAsync(userId) is not Ngo ngo)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            List<string> categories = await _categoryQueryService.GetCategoryNameByNgoAsync(ngo.Id);
            float average_rating = await _reviewQueryService.GetAverageRatingByNgoAsync(ngo.Id);

            List<EventToCard> upcomingEvents = await _eventQueryService.GetUpcomingEventsByNgoAsync(HttpContext, ngo.Id);
            List<EventToCard> activeEvents = await _eventQueryService.GetActiveEventsByNgoAsync(HttpContext, ngo.Id);
            List<EventToCard> endedEvents = await _eventQueryService.GetEndedEventsByNgoAsync(HttpContext, ngo.Id);

            NgoPageInfo ngoPageInfo = new NgoPageInfo(ngo, categories, average_rating, upcomingEvents, activeEvents, endedEvents);
                        
            return StatusCode(StatusCodes.Status200OK, ngoPageInfo);
        }

        /// <summary>
        /// NGO page (Volunteer View)    
        /// </summary>                     
        /// <response code="200">User authenticated</response>
        /// <response code="401">Access token is invalid</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(AuthenticationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]        
        [HttpGet("{id}")]
        public async Task<ActionResult> NgoPageAsync([FromQuery]Guid id)
        {            
            if (await _ngoQueryService.GetByIdAsync(id) is not Ngo ngo)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            List<string> categories = await _categoryQueryService.GetCategoryNameByNgoAsync(id);
            float average_rating = await _reviewQueryService.GetAverageRatingByNgoAsync(id);

            List<EventToCard> upcomingEvents = await _eventQueryService.GetUpcomingEventsByNgoAsync(HttpContext, ngo.Id);
            List<EventToCard> activeEvents = await _eventQueryService.GetActiveEventsByNgoAsync(HttpContext, ngo.Id);
            List<EventToCard> endedEvents = await _eventQueryService.GetEndedEventsByNgoAsync(HttpContext, ngo.Id);

            NgoPageInfo ngoPageInfo = new NgoPageInfo(ngo, categories, average_rating, upcomingEvents, activeEvents, endedEvents);
            return StatusCode(StatusCodes.Status501NotImplemented, ngoPageInfo);
        }

        /// <summary>
        /// Get ngos by category (Volunteer)
        /// </summary>                
        /// <param name="id">Category id</param>
        /// <response code="501">Not implemented</response>
        //[ProducesResponseType(StatusCodes.Status501NotImplemented)]
        //[HttpGet("category/{id}")]
        //public IActionResult GetNgosByCategory([FromQuery] int id)
        //{
        //    return StatusCode(StatusCodes.Status501NotImplemented);
        //}
    }
}
