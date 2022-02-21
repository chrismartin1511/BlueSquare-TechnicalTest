using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using JobService.Dtos;
using Microsoft.Extensions.Configuration;

namespace JobService.SyncDataServices.Http
{
    public class HttpJobTransactionDataClient : IJobTransactionDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HttpJobTransactionDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task SendJobToJobTransaction(JobTransactionDto jobTransaction)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(jobTransaction),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync($"{_configuration["JobTransactionService"]}", httpContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync POST to JobTransactionService was OK!");
            }
            else
            {
                Console.WriteLine("--> Sync POST to JobTransactionService was NOT OK!");
            }
        }

        // SendGetNewJobToJobTransaction()
    }
}