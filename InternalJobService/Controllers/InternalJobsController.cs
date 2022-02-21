using System.Collections.Generic;
using System.Threading.Tasks;
using InternalJobService.Models;
using InternalJobService.Services;
using Microsoft.AspNetCore.Mvc;

namespace JobStoreApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class InternalJobsController : ControllerBase
    {
        private readonly JobsService _jobsService;

        public InternalJobsController(JobsService jobsService) =>
            _jobsService = jobsService;

        [HttpGet]
        public async Task<List<Job>> Get() =>
            await _jobsService.GetAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Job>> Get(string jobId)
        {
            var job = await _jobsService.GetAsync(jobId);

            if (job is null)
            {
                return NotFound();
            }

            return job;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Job newJob)
        {
            await _jobsService.CreateAsync(newJob);

            return CreatedAtAction(nameof(Get), new { id = newJob.Id }, newJob);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string jobId, Job updatedJob)
        {
            var job = await _jobsService.GetAsync(jobId);

            if (job is null)
            {
                return NotFound();
            }

            updatedJob.JobId = job.JobId;

            await _jobsService.UpdateAsync(jobId, updatedJob);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var job = await _jobsService.GetAsync(id);

            if (job is null)
            {
                return NotFound();
            }

            await _jobsService.RemoveAsync(id);

            return NoContent();
        }
    }
}