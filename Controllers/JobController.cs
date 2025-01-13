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
    public class JobController : Controller
    {
        private readonly IJobService _jobService;

        public JobController(IJobService jobService)
        {
            _jobService = jobService;
        }

        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var jobs = await _jobService.GetAllJobsAsync();
            return View("Index", jobs);
        }

        [HttpGet]
        public async Task<IActionResult> GetJobs() =>
            Ok(await _jobService.GetAllJobsAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetJob(int id)
        {
            var job = await _jobService.GetJobByIdAsync(id);
            return job == null ? NotFound() : Ok(job);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateJobRequest jobRequest)
        {
            if (ModelState.IsValid)
            {
                Job job = new Job()
                {
                    Title = jobRequest.Title
                };

                await _jobService.AddJobAsync(job);
                return Json(new { success = true, redirectUrl = Url.Action("Index", "Job") });
            }

            return View(jobRequest);
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            Job job = await _jobService.GetJobByIdAsync(id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        [HttpPost("Edit/{id}")]
        public async Task<IActionResult> Edit(int id, EditJobRequest jobRequest)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Job job = await _jobService.GetJobByIdAsync(id);
                    if (job == null)
                    {
                        ModelState.AddModelError("", "Job not found.");
                        return View(jobRequest);
                    }

                    job.Title = jobRequest.Title;

                    await _jobService.UpdateJobAsync(job);

                    return Json(new { success = true, redirectUrl = Url.Action("Index", "Job") });
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "An error occurred while saving the job.");
                }
            }

            return View(jobRequest);
        }

        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Job job = await _jobService.GetJobByIdAsync(id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);

        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _jobService.DeleteJobAsync(id);
            return Json(new { success = true, redirectUrl = Url.Action("Index", "Job") });
        }
    }
}
