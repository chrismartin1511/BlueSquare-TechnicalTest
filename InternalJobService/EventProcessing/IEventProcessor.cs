namespace InternalJobService.EventProcessing
{
    public interface IEventProcessor
    {
        void ProcessEvent(string message);
    }
}