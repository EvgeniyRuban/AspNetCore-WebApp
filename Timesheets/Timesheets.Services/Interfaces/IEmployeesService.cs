using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Timesheets.Entities;
using Timesheets.Entities.Dto;

namespace Timesheets.Services.Interfaces
{
    public interface IEmployeesService
    {
        Task<Employee> GetAsync(int id, CancellationToken cancelToken);
        Task<IReadOnlyCollection<Employee>> GetRangeAsync(int skip, int take, CancellationToken cancelToken);
        Task AddAsync(CreateEmployeeRequest request, CancellationToken cancelToken);
        Task UpdateAsync(Employee employeeToUpdate, CancellationToken cancelToken);
        Task DeleteAsync(int id, CancellationToken cancelToken);
    }
}
