using System.Collections.Generic;
using JobService.Models;

namespace JobService.Data
{
    // this should be for querying client DB
    public interface IExternalJobRepo
    {
        IEnumerable<Job> GetAllExternalJobs();

        Job GetExternalJobById(int JobId);

        void UpdateExternalJobStatus(Job job, int id);

        public IEnumerable<Job> GetNewJobs();

        bool SaveChanges();
    }
}