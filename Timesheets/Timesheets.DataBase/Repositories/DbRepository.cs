using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Timesheets.Entities;

namespace Timesheets.DataBase.Repositories
{
    public sealed class DbRepository<TEntity> : IDbRepository<TEntity> where TEntity : Entity
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
