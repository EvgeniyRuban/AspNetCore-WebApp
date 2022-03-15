using System.Threading;
using System.Threading.Tasks;
using Timesheets.Entities;

namespace Timesheets.DataBase.Repositories.Interfaces
{
    public interface IContractsRepository : IBaseRepository<Contract>
    {
        Task<bool?> CheckContractIsActive(int id, CancellationToken cancelToken);
        Task DeleteAsync(int id, CancellationToken cancelToken);
    }
}
