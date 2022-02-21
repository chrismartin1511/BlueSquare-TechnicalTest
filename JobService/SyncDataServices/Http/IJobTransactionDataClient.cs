using System.Threading.Tasks;
using JobService.Dtos;

namespace JobService.SyncDataServices.Http
{
    public interface IJobTransactionDataClient
    {
        Task SendJobToJobTransaction(JobTransactionDto jobTransaction);
    }
}