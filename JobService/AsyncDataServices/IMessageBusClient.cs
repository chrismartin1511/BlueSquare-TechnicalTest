using JobService.Dtos;

namespace JobService.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void PublishJobTransaction(JobTransactionPublishedDto jobTransactionPublishedDto);
    }
}