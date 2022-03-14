using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Timesheets.Entities;

namespace Timesheets.DataBase.Repositories.Interfaces
{
    /// <summary>
    /// Methods for employees data managnent at the database level.
    /// </summary>
    public interface IEmployeesRepository
    {
        Task<Employee> GetAsync(Guid id, CancellationToken cancelToken);
        Task<List<Employee>> GetRangeAsync(int skip, int take, CancellationToken cancelToken);
        Task AddAsync(Employee employee, CancellationToken cancelToken);
        Task UpdateAsync(Employee newEmployee, CancellationToken cancelToken);
        Task DeleteAsync(Guid id, CancellationToken cancelToken);
    }
}
