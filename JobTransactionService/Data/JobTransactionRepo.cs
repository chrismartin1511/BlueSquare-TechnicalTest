using System;
using System.Collections.Generic;
using System.Linq;
using JobTransactionService.Models;

namespace JobTransactionService.Data
{
    public class JobTransactionRepo : IJobTransactionRepo
    {
        private readonly AppDbContext _context;

        public JobTransactionRepo(AppDbContext context)
        {
            _context = context;
        }

        public void CreateJobTransaction(JobTransaction jobTransaction)
        {
            if (jobTransaction == null)
            {
                Console.WriteLine("NULL");
                throw new ArgumentNullException(nameof(jobTransaction));
            }

            Console.WriteLine(jobTransaction.JobId);

            _context.JobTransactions.Add(jobTransaction);
        }

        public IEnumerable<JobTransaction> GetAllTransactions() => _context.JobTransactions.ToList();

        public JobTransaction GetJobsLatestTransaction(int? jobId)
        {
            Console.WriteLine($"--> Getting Jobs Latest Transaction: {jobId}");

            return _context.JobTransactions.Where(j => j.JobId == jobId).LastOrDefault();
        }

        public bool JobTransactionExists(int jobId)
        {
            Console.WriteLine($"--> Checking if Job Transaction Exists: {jobId}");

            return _context.JobTransactions.Any(j => j.JobId == jobId);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}