using System;
using System.Threading;
using System.Threading.Tasks;
using Timesheets.Entities;

namespace Timesheets.DataBase.Repositories.Interfaces
{
    public interface IClientsRepository : IBaseRepository<Client>
    {
        Task DeleteAsync(Guid id, CancellationToken cancelToken);
    }
}
