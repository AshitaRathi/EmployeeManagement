using EmployeeManagement.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly ITokenService _tokenService;
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthController(IEmployeeService employeeService, ITokenService tokenService, IHttpClientFactory httpClientFactory)
        {
            _employeeService = employeeService;
            _tokenService = tokenService;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("Login")]
        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(string Email, string Password)
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                ViewBag.ErrorMessage = "Email and Password are required.";
                return View("Login");
            }

            var employee = await _employeeService.LoginCheckAsync(Email, Password);
            if (employee != null)
            {
                var token = _tokenService.GenerateToken(employee);

                HttpContext.Session.SetString("EmployeeToken", token);
                string tokenSent = HttpContext.Session.GetString("EmployeeToken");

                if (string.IsNullOrEmpty(tokenSent))
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid email or password.";
                return View("Login");
            }
        }
    }
}

