using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Timesheets.DataBase.Repositories.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<T> GetAsync(int id, CancellationToken cancelToken);
        Task<IReadOnlyCollection<T>> GetRangeAsync(int skip, int take, CancellationToken cancelToken);
        Task<T> CreateAsync(T item, CancellationToken cancelToken);
        Task UpdateAsync(T item, CancellationToken cancelToken);
    }
}
