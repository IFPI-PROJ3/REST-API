using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proj3.Application.Common.Interfaces.Services.Common.Queries;
using Proj3.Application.Common.Interfaces.Services.NGO.Commands;
using Proj3.Application.Common.Interfaces.Services.NGO.Queries;
using Proj3.Application.Common.Interfaces.Services.Volunteer.Queries;
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
        private readonly ICategoryQueryService _categoryQueryService;
        private readonly IReviewQueryService _reviewQueryService;

        public NGOController(INgoCommandService ngoCommandService , INgoQueryService ngoQueryService, ICategoryQueryService categoryQueryService, IReviewQueryService reviewQueryService)
        {
            _ngoCommandService = ngoCommandService;            
            _ngoQueryService = ngoQueryService;
            _categoryQueryService = categoryQueryService;
            _reviewQueryService = reviewQueryService;
        }

        /// <summary>
        /// Initial page (NGO profile View)    
        /// </summary>                     
        /// <response code="200">User authenticated</response>
        /// <response code="401">Access token is invalid</response>
        /// <response code="401">Refresh token is invalid</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(AuthenticationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]        
        [HttpGet("initial-page")]
        public async Task<ActionResult> InitialPageAsync()
        {
            Guid userId = Application.Utils.Authentication.User.GetUserIdFromHttpContext(HttpContext);            

            Ngo? ngo = await _ngoQueryService.GetByUserId(userId);
            List<string> categories = await _categoryQueryService.GetCategoryNameByNgo(ngo.Id);
            //float average_rating = await _reviewQueryService.GetAverageRatingByNgoAsync(ngo.Id);

            NgoPageInfo ngoPageInfo = new NgoPageInfo(ngo, categories, 0);
            return StatusCode(StatusCodes.Status200OK, ngoPageInfo);
        }

        /// <summary>
        /// NGO page (Volunteer NGO profile View)    
        /// </summary>                     
        /// <response code="200">User authenticated</response>
        /// <response code="401">Access token is invalid</response>
        /// <response code="401">Refresh token is invalid</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(AuthenticationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]        
        [HttpGet("{id}")]
        public async Task<ActionResult> NgoPageAsync([FromQuery]Guid id)
        {
            Ngo ngo = await _ngoQueryService.GetByUserId(id);
            List<string> categories = await _categoryQueryService.GetCategoryNameByNgo(id);
            float average_rating = await _reviewQueryService.GetAverageRatingByNgoAsync(id);
            
            NgoPageInfo ngoPageInfo = new NgoPageInfo(ngo, categories, average_rating);
            return StatusCode(StatusCodes.Status501NotImplemented, ngoPageInfo);
        }
    }
}
