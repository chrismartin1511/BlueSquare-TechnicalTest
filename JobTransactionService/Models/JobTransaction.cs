using System;
using System.ComponentModel.DataAnnotations;

namespace JobTransactionService.Models
{
    public class JobTransaction
    {
        [Key]
        [Required]
        public int JobId { get; set; }

        [Required]
        public string JobStatus { get; set; }

        public DateTime CreationDate { get; set; }

        public string UpdateNote { get; set; }
    }
}