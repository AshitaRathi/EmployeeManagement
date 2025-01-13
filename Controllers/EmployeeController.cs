using EmployeeManagement.Models;
using EmployeeManagement.Request;
using EmployeeManagement.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IJobService _jobService;
        private readonly ITeamService _teamService;

        public EmployeeController(IEmployeeService employeeService, ITeamService teamService, IJobService jobService)
        {
            _employeeService = employeeService;
            _jobService = jobService;
            _teamService = teamService;
        }

        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return View("Index", employees);
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees() =>
            Ok(await _employeeService.GetAllEmployeesAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            return employee == null ? NotFound() : Ok(employee);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            ViewData["Jobs"] = _jobService.GetAllJobsAsync().Result;
            ViewData["Teams"] = _teamService.GetAllTeamsAsync().Result;
            return View();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateEmployeeRequest employeeRequest)
        {
            if (ModelState.IsValid)
            {
                Employee employee = new Employee()
                {
                    Password = employeeRequest.Password,
                    Name = employeeRequest.Name,
                    Gender = employeeRequest.Gender,
                    PhoneNumber = employeeRequest.PhoneNumber,
                    Email = employeeRequest.Email,
                    DateOfBirth = employeeRequest.DateOfBirth,
                    HireDate = employeeRequest.HireDate,
                    TotalExperience = employeeRequest.TotalExperience
                };

                var job = await _jobService.GetJobByIdAsync(employeeRequest.JobId);
                var team = await _teamService.GetTeamByIdAsync(employeeRequest.TeamId);

                if (job == null || team == null)
                {
                    ModelState.AddModelError("", "Invalid Job or Team.");
                    ViewData["Jobs"] = await _jobService.GetAllJobsAsync();
                    ViewData["Teams"] = await _teamService.GetAllTeamsAsync();
                    return View(employeeRequest);
                }

                employee.Job = job;
                employee.Team = team;

                await _employeeService.AddEmployeeAsync(employee);
                return Json(new { success = true, redirectUrl = Url.Action("Index", "Employee") });
            }

            ViewData["Jobs"] = await _jobService.GetAllJobsAsync();
            ViewData["Teams"] = await _teamService.GetAllTeamsAsync();
            return View(employeeRequest);
        }


        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            Employee employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            ViewData["Jobs"] = await _jobService.GetAllJobsAsync();
            ViewData["Teams"] = await _teamService.GetAllTeamsAsync();

            var employeeEditRequest = new EditEmployeeRequest
            {
                Id = employee.Id,
                Name = employee.Name,
                Gender = employee.Gender,
                PhoneNumber = employee.PhoneNumber,
                Email = employee.Email,
                TotalExperience = employee.TotalExperience,
                JobId = employee.Job.Id,
                TeamId = employee.Team.Id,
                DateOfBirth = employee.DateOfBirth,
                HireDate = employee.HireDate
            };

            return View(employeeEditRequest);
        }

        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditEmployeeRequest employeeRequest)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var job = await _jobService.GetJobByIdAsync(employeeRequest.JobId);
                    var team = await _teamService.GetTeamByIdAsync(employeeRequest.TeamId);

                    if (job == null || team == null)
                    {
                        ModelState.AddModelError("", "Invalid Job or Team.");
                        ViewData["Jobs"] = await _jobService.GetAllJobsAsync();
                        ViewData["Teams"] = await _teamService.GetAllTeamsAsync();
                        return View(employeeRequest);
                    }

                    Employee employee = new Employee
                    {
                        Id = id,
                        Name = employeeRequest.Name,
                        Password = employeeRequest.Password,
                        Gender = employeeRequest.Gender,
                        PhoneNumber = employeeRequest.PhoneNumber,
                        Email = employeeRequest.Email,
                        TotalExperience = employeeRequest.TotalExperience,
                        Job = job,
                        Team = team,
                        DateOfBirth = employeeRequest.DateOfBirth,
                        HireDate = employeeRequest.HireDate
                    };

                    await _employeeService.UpdateEmployeeAsync(employee);
                    return Json(new { success = true, redirectUrl = Url.Action("Index", "Employee") });

                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "An error occurred while saving the employee.");
                }
            }

            ViewData["Jobs"] = await _jobService.GetAllJobsAsync();
            ViewData["Teams"] = await _teamService.GetAllTeamsAsync();
            return View(employeeRequest);
        }

        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Employee employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);

        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _employeeService.DeleteEmployeeAsync(id);
            return Json(new { success = true, redirectUrl = Url.Action("Index", "Employee") });
        }

    }
}
