using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAndAuthorization.Controllers
{
    [Authorize(Policy = "MustTeacher")]
    public class TeacherRoomController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
