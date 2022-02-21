using System.ComponentModel.DataAnnotations;

namespace JobService.Dtos
{
    // Writes to JobTransaction service
    public class JobTransactionDto
    {
        [Required]
        public int JobId { get; set; }

        [Required]
        public string JobStatus { get; set; }

        public string UpdateNote { get; set; }
    }
}