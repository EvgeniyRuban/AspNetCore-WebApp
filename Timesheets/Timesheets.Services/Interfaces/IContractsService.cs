using System.Threading;
using System.Threading.Tasks;

namespace Timesheets.Services.Interfaces
{
    public interface IContractsService
    {
        Task<bool?> CheckContractIsActiveAsync(int id, CancellationToken cancelToken);
    }
}
