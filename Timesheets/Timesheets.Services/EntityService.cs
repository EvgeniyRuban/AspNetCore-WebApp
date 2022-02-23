using Timesheets.Entities;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Collections.Generic;

namespace Timesheets.Services
{
    public sealed class EntityService<TEntity> : IEntityService<TEntity> where TEntity : Entity
    {
        public Task AddAsync(TEntity entety, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid id, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetAsync(Guid id, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> GetRangeAsync(int skip, int take, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(TEntity entety, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
