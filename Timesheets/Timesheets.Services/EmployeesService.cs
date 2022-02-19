using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Timesheets.DataBase.Repositories;
using Timesheets.Entities;

namespace Timesheets.Services
{
    public class EmployeesService : IEntityService<Employee>
    {
        private IDbRepository<Employee> _repository;

        public EmployeesService(IDbRepository<Employee> repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(Employee entity, CancellationToken token)
        {
            await _repository.AddAsync(entity, token);
        }

        public async Task DeleteAsync(int id, CancellationToken token)
        {
            await _repository.DeleteAsync(id, token);
        }

        public async Task<Employee> GetAsync(int id, CancellationToken token)
        {
            return await _repository.GetAsync(id, token);
        }

        public async Task<IReadOnlyCollection<Employee>> GetRangeAsync(int skip, int take, CancellationToken token)
        {
            return await _repository.GetRangeAsync(skip, take, token);
        }

        public async Task UpdateAsync(Employee entity, CancellationToken token)
        {
            await _repository.UpdateAsync(entity, token);
        }
    }
}
