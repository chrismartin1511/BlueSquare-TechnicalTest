using System;

namespace JobTransactionService.Dtos
{
    public class JobTransactionReadDto
    {
        public int JobId { get; set; }

        public string JobStatus { get; set; }

        public DateTime CreationDate { get; set; }

        public string UpdateNote { get; set; }
    }
}