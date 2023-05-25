using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proj3.Application.Common.Interfaces.Services.NGO.Commands;
using Proj3.Application.Common.Interfaces.Services.NGO.Queries;
using Proj3.Application.Services.NGO.Commands;
using Proj3.Contracts.NGO.Request;
using Proj3.Domain.Entities.NGO;

namespace Proj3.Api.Controllers.Category
{
    [ApiController]
    [Authorize]
    [Route("category")]
    [ApiVersion("1.0")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class CategoryController : ControllerBase
    {
        //private readonly ICategoryQueryService _categoryQueryService;

        public CategoryController(/*ICategoryQueryService categoryQueryService*/)
        {
            //_categoryQueryService = ngoCommandService;            
        }


        /// <summary>
        /// Get collections with all categories
        /// </summary>          
        /// <response code="200">Category collection</response>        
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("all")]
        public async Task<IActionResult> All([FromQuery] NewNgoRequest request)
        {
            //return _categoryQueryService.All();
            return Ok(StatusCodes.Status200OK);
        }
    }
}
