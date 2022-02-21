using JobService.Models;
using Microsoft.EntityFrameworkCore;

namespace JobService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {
            
        }

        public DbSet<Job> Jobs { get; set;}

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.Entity<Job>(entity =>
        //     {
        //         entity.ToTable("jobs");
        //         entity.Property(p => p.JobId).HasColumnName("job_id");
        //         entity.Property(p => p.JobType).HasColumnName("job_type");
        //         entity.Property(p => p.JobStatus).HasColumnName("job_status");
        //         entity.Property(p => p.ClientFirstName).HasColumnName("client_first_name");
        //         entity.Property(p => p.ClientLastName).HasColumnName("client_last_name");
        //         entity.Property(p => p.ClientPostCode).HasColumnName("client_post_code");
        //         entity.Property(p => p.ClientMobile).HasColumnName("client_mobile");
        //         entity.Property(p => p.ProductId).HasColumnName("product_id");
        //         entity.Property(p => p.ProductType).HasColumnName("product_type");
        //     });
        // }
    }
}