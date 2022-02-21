using System.ComponentModel.DataAnnotations;

namespace JobService.Dtos
{
    // Write to MongoDb
    public class JobPublishedDto
    {
        public int JobId { get; set; }

        public string JobType { get; set; }

        [Required]
        public string JobStatus { get; set; }

        public string ClientFirstName { get; set; }

        public string ClientLastName { get; set; }

        public string ClientPostCode { get; set; }

        public string ClientMobile { get; set; }

        public string ProductId { get; set; }
        
        public string ProductType { get; set; }
    }
}