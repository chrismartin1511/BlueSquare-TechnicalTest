using System.ComponentModel.DataAnnotations;

namespace JobService.Models
{
    // Reads from external client DB
    public class Job
    {
        // [Key]
        // [Required]
        // public int Id { get; set; }
        [Key]
        [Required]
        public int JobId { get; set; }

        [Required]
        public string JobType { get; set; }

        [Required]
        public string JobStatus { get; set; }

        [Required]
        public string ClientFirstName { get; set; }

        [Required]
        public string ClientLastName { get; set; }

        [Required]
        public string ClientPostCode { get; set; }

        [Required]
        public string ClientMobile { get; set; }

        [Required]
        public string ProductId { get; set; }

        [Required]
        public string ProductType { get; set; }
    }
}