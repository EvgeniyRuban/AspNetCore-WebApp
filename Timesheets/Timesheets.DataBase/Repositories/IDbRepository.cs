using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using Timesheets.Entities;

namespace Timesheets.DataBase.Repositories
{
    public interface IDbRepository<TEntity> where TEntity : Entity
    {
        Task AddAsync(TEntity entety, CancellationToken token);
        Task<TEntity> GetAsync(Guid id, CancellationToken token);
        Task<IEnumerable<TEntity>> GetRangeAsync(int skip, int take, CancellationToken token);
        Task<bool> UpdateAsync(TEntity entety, CancellationToken token);
        Task<bool> DeleteAsync(Guid id, CancellationToken token);
    }
}
