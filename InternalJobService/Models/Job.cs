using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace InternalJobService.Models
{
    public class Job
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string JobId { get; set; }

        public string JobType { get; set; }

        public string JobStatus { get; set; }

        public string ClientFirstName { get; set; }

        public string ClientLastName { get; set; }

        public string ClientPostCode { get; set; }

        public string ClientMobile { get; set; }

        public string ProductId { get; set; }

        public string ProductType { get; set; }
    }
}