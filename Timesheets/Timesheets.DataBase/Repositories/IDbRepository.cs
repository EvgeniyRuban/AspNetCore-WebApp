using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using Timesheets.Entities;

namespace Timesheets.DataBase.Repositories
{
    public interface IDbRepository<TEntity> where TEntity : Entity
    {
        Task AddAsync(TEntity entity, CancellationToken token);
        Task<TEntity> GetAsync(int id, CancellationToken token);
        Task<IReadOnlyCollection<TEntity>> GetRangeAsync(int skip, int take, CancellationToken token);
        Task UpdateAsync(TEntity newEntity, CancellationToken token);
        Task DeleteAsync(int id, CancellationToken token);
    }
}
