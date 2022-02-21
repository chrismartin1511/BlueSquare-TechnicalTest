using System.Collections.Generic;
using System.Threading.Tasks;
using InternalJobService.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace InternalJobService.Services
{

    public class JobsService
    {
        private readonly IMongoCollection<Job> _JobsCollection;

        public JobsService(IOptions<InternalJobDatabaseSettings> internalJobDatabaseSettings)
        {
            var mongoClient = new MongoClient(internalJobDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(internalJobDatabaseSettings.Value.DatabaseName);

            _JobsCollection = mongoDatabase.GetCollection<Job>(internalJobDatabaseSettings.Value.JobsCollectionName);
        }

        public async Task<List<Job>> GetAsync() => await _JobsCollection.Find(_ => true).ToListAsync();

        public async Task<Job> GetAsync(string id) => await _JobsCollection.Find(x => x.JobId == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Job newJob) => await _JobsCollection.InsertOneAsync(newJob);

        public async Task UpdateAsync(string id, Job updatedJob) => await _JobsCollection.ReplaceOneAsync(x => x.JobId == id, updatedJob);

        public async Task RemoveAsync(string id) => await _JobsCollection.DeleteOneAsync(x => x.JobId == id);
    }
}