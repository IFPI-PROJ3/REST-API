using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proj3.Application.Common.Interfaces.Services.Common.Queries;
using System.Net.Mime;

namespace Proj3.Api.Controllers.Common
{
    /// <summary>
    /// Category controller
    /// </summary>
    [ApiController]    
    [Route("category")]
    [ApiVersion("1.0")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryQueryService _categoryQueryService;

        /// <summary>
        /// Category constructor
        /// </summary>
        public CategoryController(ICategoryQueryService categoryQueryService)
        {
            _categoryQueryService = categoryQueryService;            
        }

        /// <summary>
        /// Get collections with all categories
        /// </summary>          
        /// <response code="200">Categories collection</response>        
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("/all")]
        public IActionResult All()
        {
            var categories = _categoryQueryService.GetAllAsync();
            return StatusCode(StatusCodes.Status200OK, categories);
        }
    }
}
