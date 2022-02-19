using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Timesheets.Entities;

namespace Timesheets.Services
{
    public interface IEntityService<TEntity> where TEntity : Entity
    {
        Task AddAsync(TEntity entity, CancellationToken token);
        Task<TEntity> GetAsync(int id, CancellationToken token);
        Task<IReadOnlyCollection<TEntity>> GetRangeAsync(int skip, int take, CancellationToken token);
        Task UpdateAsync(TEntity entity, CancellationToken token);
        Task DeleteAsync(int id, CancellationToken token);
    }
}
