using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class HomeController : Controller
    {
        [Route("Index")]
        public IActionResult Index()
        {
            var employeeToken = HttpContext.Session.GetString("EmployeeToken");

            if (string.IsNullOrEmpty(employeeToken))
            {
                Console.WriteLine("Session ID is null or empty.");
                return RedirectToAction("Login", "Auth");
            }

            return View();
        }
    }
}

