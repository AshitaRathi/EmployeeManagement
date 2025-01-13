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
    public class TeamController : Controller
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var teams = await _teamService.GetAllTeamsAsync();
            return View("Index", teams);
        }

        [HttpGet]
        public async Task<IActionResult> GetJobs() =>
            Ok(await _teamService.GetAllTeamsAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetJob(int id)
        {
            var team = await _teamService.GetTeamByIdAsync(id);
            return team == null ? NotFound() : Ok(team);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateTeamRequest teamRequest)
        {
            if (ModelState.IsValid)
            {
                Team team = new Team()
                {
                    Name = teamRequest.Name
                };

                await _teamService.AddTeamAsync(team);
                return Json(new { success = true, redirectUrl = Url.Action("Index", "Team") });
            }

            return View(teamRequest);
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            Team team = await _teamService.GetTeamByIdAsync(id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditTeamRequest teamRequest)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Team team = await _teamService.GetTeamByIdAsync(id);
                    if (team == null)
                    {
                        ModelState.AddModelError("", "Team not found.");
                        return View(teamRequest);
                    }

                    team.Name = teamRequest.Name;

                    await _teamService.UpdateTeamAsync(team);

                    return Json(new { success = true, redirectUrl = Url.Action("Index", "Team") });
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "An error occurred while saving the team.");
                }
            }

            return View(teamRequest);
        }


        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Team team = await _teamService.GetTeamByIdAsync(id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);

        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _teamService.DeleteTeamAsync(id);
            return Json(new { success = true, redirectUrl = Url.Action("Index", "Team") });
        }
    }
}
