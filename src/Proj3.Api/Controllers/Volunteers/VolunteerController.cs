using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Proj3.Api.Controllers.Volunteers
{    
    [ApiController]    
    [Route("volunteer")]
    [ApiVersion("1.0")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class VolunteerController : ControllerBase
    {        
    }
}
