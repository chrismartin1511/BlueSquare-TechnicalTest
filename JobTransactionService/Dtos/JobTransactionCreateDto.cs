using System.ComponentModel.DataAnnotations;

namespace JobTransactionService.Dtos
{
    public class JobTransactionCreateDto
    {     
        [Required]  
        public int JobId { get; set; }

        [Required]
        public string JobStatus { get; set; }

        public string UpdateNote { get; set; }
    }
}