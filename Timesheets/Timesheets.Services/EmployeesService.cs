using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Timesheets.DataBase.Repositories;
using Timesheets.DataBase.Repositories.Interfaces;
using Timesheets.Entities;
using Timesheets.Entities.Dto;
using Timesheets.Services.Interfaces;

namespace Timesheets.Services
{ 
    public class EmployeesService : IEmployeesService
    {
        private readonly IEmployeesRepository _repository;

        public EmployeesService(IEmployeesRepository repository)
        {
            _repository = repository;
        }

        public async Task<Employee> GetAsync(int id, CancellationToken cancelToken)
        {
            return await _repository.GetAsync(id, cancelToken);
        }
        public async Task<IReadOnlyCollection<Employee>> GetRangeAsync(int skip, int take, CancellationToken cancelToken)
        {
            return await _repository.GetRangeAsync(skip, take, cancelToken);
        }
        public async Task AddAsync(CreateEmployeeRequest request, CancellationToken cancelToken)
        {
            var employee = new Employee
            {
                UserId = request.UserId,
            };
            await _repository.AddAsync(employee, cancelToken);
        }
        public async Task UpdateAsync(Employee employeeToUpdate, CancellationToken cancelToken)
        {
            await _repository.UpdateAsync(employeeToUpdate, cancelToken);
        }
        public async Task DeleteAsync(int id, CancellationToken cancelToken)
        {
            await _repository.DeleteAsync(id, cancelToken);
        }
    }
}
