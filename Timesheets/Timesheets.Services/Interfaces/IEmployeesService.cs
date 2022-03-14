using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Timesheets.Entities;
using Timesheets.Entities.Dto;

namespace Timesheets.Services.Interfaces
{
    /// <summary>
    /// Methods for employee repository managnent at the business logic level.
    /// </summary>
    public interface IEmployeesService
    {
        Task<EmployeeResponse> GetAsync(Guid id, CancellationToken cancelToken);
        Task<IReadOnlyCollection<EmployeeResponse>> GetRangeAsync(int skip, int take, CancellationToken cancelToken);
        Task AddAsync(CreateEmployeeRequest request, CancellationToken cancelToken);
        Task UpdateAsync(EmployeeRequest request, CancellationToken cancelToken);
        Task DeleteAsync(Guid id, CancellationToken cancelToken);
    }
}
