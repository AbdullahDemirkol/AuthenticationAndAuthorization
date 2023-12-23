using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAndAuthorization.Controllers
{
    public class ErrorController : Controller
    {
        [Route("/error/{statusCode}")]
        public IActionResult HandleError(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    return Redirect("/error/NotFound");
                default:
                    return Redirect("/error/Error");
            }
        }
        [Route("/error/NotFound")]
        public IActionResult NotFound()
        {
            return View();
        }
        [Route("/error/Error")]
        public IActionResult Error()
        {
            return View();
        }
    }
}
