using System;
using System.Collections.Generic;
using System.Linq;
using JobService.Models;

namespace JobService.Data
{
    public class ExternalJobRepo : IExternalJobRepo
    {
        private readonly AppDbContext _context;

        public ExternalJobRepo(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Job> GetAllExternalJobs() => _context.Jobs.ToList();

        public Job GetExternalJobById(int id) => _context.Jobs.FirstOrDefault(j => j.JobId == id);

        public void UpdateExternalJobStatus(Job job, int jobId)
        {
            if (job == null)
            {
                throw new ArgumentNullException(nameof(job));
            }
            
             var jobToUpdate = GetExternalJobById(jobId);

             jobToUpdate.JobStatus = job.JobStatus;

            _context.Jobs.Update(jobToUpdate);
        }

        public IEnumerable<Job> GetNewJobs() => _context.Jobs.ToList().Where(j => j.JobStatus == "New");

        public bool SaveChanges() => (_context.SaveChanges() >= 0);
    }
}