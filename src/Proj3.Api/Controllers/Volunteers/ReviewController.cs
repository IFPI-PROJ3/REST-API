using Microsoft.AspNetCore.Mvc;

namespace Proj3.Api.Controllers.Volunteers
{
    public class ReviewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
