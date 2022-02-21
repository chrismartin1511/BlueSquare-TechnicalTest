using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobService.AsyncDataServices;
using JobService.Data;
using JobService.SyncDataServices.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace JobService
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {

        Console.WriteLine("--> Using InMem Db");
        services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMem"));

        // MySql Database setup
        Console.WriteLine("--> Using MySql Db");
        // var connectionString = Configuration.GetConnectionString("ExternalJobConnection");
        // Console.WriteLine($"--> Connection String: {connectionString}");

        // var serverVersion = ServerVersion.AutoDetect(connectionString);


        // services.AddDbContext<AppDbContext>(dbContextOptions => dbContextOptions
        //         .UseMySql(connectionString, serverVersion)
        //         // The following three options help with debugging, but should
        //         // be changed or removed for production.
        //         .LogTo(Console.WriteLine, LogLevel.Information)
        //         .EnableSensitiveDataLogging()
        //         .EnableDetailedErrors());
                    
        services.AddScoped<IExternalJobRepo, ExternalJobRepo>();

        services.AddHttpClient<IJobTransactionDataClient, HttpJobTransactionDataClient>();
        services.AddSingleton<IMessageBusClient, MessageBusClient>();

        services.AddControllers();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddSwaggerGen(c =>
        {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "JobService", Version = "v1" });
        });

        Console.WriteLine($"--> JobTransaction Endpoint {Configuration["JobTransactionService"]}");
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "JobService v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            PrepDb.PrepPopulation(app, env.IsProduction());
        }
    }
}
