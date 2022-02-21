using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using JobService.Models;

namespace JobService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app, bool isProd)
        {
            using( var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProd);
            }
        }

        private static void SeedData(AppDbContext context, bool isProd)
        {            
            if(!context.Jobs.Any())
            {
                Console.WriteLine("--> Seeding Data...");

                context.Jobs.AddRange(
                    new Job() {JobId=1, JobType="repair", JobStatus="New", ClientFirstName="Joe", ClientLastName="Bloggs", ClientPostCode="SG13 7TZ", ClientMobile="+44770123456", ProductId="1", ProductType="fridge"},
                    new Job() {JobId=2, JobType="replace", JobStatus="Job Completed", ClientFirstName="Dan", ClientLastName="Smith", ClientPostCode="SG1 3AP", ClientMobile="+44770678912", ProductId="2", ProductType="dishwasher"}
                );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> We already have data");
            }
        }
    }
}