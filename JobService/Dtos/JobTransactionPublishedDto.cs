namespace JobService.Dtos
{
    public class JobTransactionPublishedDto
    {
        public int JobId { get; set; }
        
        public string JobStatus { get; set; }

        public string UpdateNote { get; set; }
    }
}