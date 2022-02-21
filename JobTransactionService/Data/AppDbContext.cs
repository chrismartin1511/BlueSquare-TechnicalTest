using JobTransactionService.Models;
using Microsoft.EntityFrameworkCore;

namespace JobTransactionService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {
            
        }

        public DbSet<JobTransaction> JobTransactions { get; set; }
    }
}