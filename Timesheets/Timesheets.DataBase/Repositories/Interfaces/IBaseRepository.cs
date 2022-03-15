using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Timesheets.DataBase.Repositories.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<T> GetAsync(Guid id, CancellationToken cancelToken);
        Task<IReadOnlyCollection<T>> GetRangeAsync(int skip, int take, CancellationToken cancelToken);
        Task AddAsync(T item, CancellationToken cancelToken);
        Task UpdateAsync(T item, CancellationToken cancelToken);
    }
}
