using System;
using System.Threading;
using System.Threading.Tasks;
using Timesheets.Entities;

namespace Timesheets.DataBase.Repositories.Interfaces
{
    public interface IContractsRepository : IBaseRepository<Contract>
    {
        Task<bool?> CheckContractIsActive(Guid id, CancellationToken cancelToken);
        Task DeleteAsync(Guid id, CancellationToken cancelToken);
    }
}
