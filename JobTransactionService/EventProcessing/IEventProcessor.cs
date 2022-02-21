namespace JobTransactionService.EventProcessing
{
    public interface IEventProcessor
    {
        void ProcessEvent(string message);
    }
}