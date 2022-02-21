using System.Collections.Generic;
using JobTransactionService.Models;

namespace JobTransactionService.Data
{
    public interface IJobTransactionRepo
    {
        bool SaveChanges();

        IEnumerable<JobTransaction> GetAllTransactions();

        JobTransaction GetJobsLatestTransaction(int? jobId);

        void CreateJobTransaction(JobTransaction jobTransaction);
        
        // latest transaction for jobId > lastOrDefault?
        bool JobTransactionExists(int JobId);
    }
}