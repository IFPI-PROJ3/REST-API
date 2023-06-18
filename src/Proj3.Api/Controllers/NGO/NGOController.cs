using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proj3.Application.Common.Interfaces.Services.NGO.Commands;
using Proj3.Application.Common.Interfaces.Services.NGO.Queries;
using Proj3.Contracts.Authentication.Response;
using Proj3.Contracts.NGO.Request;
using Proj3.Domain.Entities.NGO;
using System.Net.Mime;

namespace Proj3.Api.Controllers.NGO
{
    [ApiController]
    [Authorize]
    [Route("ngo")]
    [ApiVersion("1.0")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class NGOController : ControllerBase
    {
        private readonly INgoCommandService _ngoCommandService;
        //private readonly INgoQueryService _ngoQueryService;        


        public NGOController(INgoCommandService ngoCommandService /*, INgoQueryService ngoQueryService*/)
        {
            _ngoCommandService = ngoCommandService;
            //_ngoQueryService = ngoQueryService;            
        }
    }
}
