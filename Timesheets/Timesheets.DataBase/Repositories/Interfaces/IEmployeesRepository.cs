using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Timesheets.Entities;

namespace Timesheets.DataBase.Repositories.Interfaces
{
    public interface IEmployeesRepository
    {
        Task<Employee> GetAsync(int id, CancellationToken cancelToken);
        Task<IReadOnlyCollection<Employee>> GetRangeAsync(int skip, int take, CancellationToken cancelToken);
        Task AddAsync(Employee employee, CancellationToken cancelToken);
        Task UpdateAsync(Employee newEmployee, CancellationToken cancelToken);
        Task DeleteAsync(int id, CancellationToken cancelToken);
    }
}
