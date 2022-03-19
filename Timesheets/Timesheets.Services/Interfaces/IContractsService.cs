using System.Threading;
using System.Threading.Tasks;
using Timesheets.Entities;

namespace Timesheets.Services.Interfaces
{
    public interface IContractsService
    {
        Task<Contract> GetAsync(int id, CancellationToken cancelToken);
        Task<bool?> CheckContractIsActiveAsync(int id, CancellationToken cancelToken);
    }
}
