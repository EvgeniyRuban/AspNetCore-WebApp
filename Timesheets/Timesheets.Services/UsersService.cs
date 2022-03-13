using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Timesheets.DataBase.Repositories;
using Timesheets.Entities;

namespace Timesheets.Services
{
    public class UsersService : IEntityService<User>
    {
        private IDbRepository<User> _repository;

        public UsersService(IDbRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(User entity, CancellationToken token)
        {
            await _repository.AddAsync(entity, token);
        }

        public async Task DeleteAsync(int id, CancellationToken token)
        {
            await _repository.DeleteAsync(id, token);
        }

        public async Task<User> GetAsync(int id, CancellationToken token)
        {
            return await _repository.GetAsync(id, token);
        }

        public Task<IReadOnlyCollection<User>> GetRangeAsync(int skip, int take, CancellationToken token)
        {
            return _repository.GetRangeAsync(skip, take, token);
        }

        public async Task UpdateAsync(User entity, CancellationToken token)
        {
            await _repository.UpdateAsync(entity, token);
        }
    }
}
