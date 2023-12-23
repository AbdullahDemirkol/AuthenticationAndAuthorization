using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAndAuthorization.Controllers
{
    [Authorize]
    public class ClassroomController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
