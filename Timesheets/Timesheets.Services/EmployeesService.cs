using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Timesheets.DataBase.Repositories.Interfaces;
using Timesheets.Entities;
using Timesheets.Entities.Dto;
using Timesheets.Services.Interfaces;

namespace Timesheets.Services
{ 
    public class EmployeesService : IEmployeesService
    {
        private readonly IEmployeesRepository _employeesRepository;
        private readonly IUsersRepository _usersRepository;

        public EmployeesService(IEmployeesRepository employeesRepository, IUsersRepository usersRepository)
        {
            _employeesRepository = employeesRepository;
            _usersRepository = usersRepository;
        }

        public async Task<EmployeeResponse> GetAsync(int id, CancellationToken cancelToken)
        {
            var employee = await _employeesRepository.GetAsync(id, cancelToken);
            if (employee != null)
            {
                return new EmployeeResponse
                {
                    Id = employee.Id,
                };
            }
            return null;
        }
        public async Task<IReadOnlyCollection<EmployeeResponse>> GetRangeAsync(int skip, int take, CancellationToken cancelToken)
        {
            var employees = await _employeesRepository.GetRangeAsync(skip, take, cancelToken);
            if (employees == null)
            {
                return null;
            }
            var employeeResponseCollection = new List<EmployeeResponse>(employees.Count);
            foreach(var employee in employees)
            {
                employeeResponseCollection.Add(await GetAsync(employee.Id, cancelToken));
            }
            return employeeResponseCollection;
        }
        public async Task<EmployeeResponse> CreateAsync(CreateEmployeeRequest request, CancellationToken cancelToken)
        {
            if(request == null)
            {
                return null;
            }
            var user = await _usersRepository.GetByIdAsync(request.UserId, cancelToken);
            if(user is null)
            {
                return null;
            }
            var employee = new Employee
            {
                UserId = request.UserId,
            };
            var newEmployee = await _employeesRepository.CreateAsync(employee, cancelToken);
            if(newEmployee != null)
            {
                return new EmployeeResponse
                {
                    Id = newEmployee.Id,
                };
            }
            return null;
        }
        public async Task UpdateAsync(EmployeeRequest request, CancellationToken cancelToken)
        {
            if(request == null)
            {
                return;
            }
            var user = await _usersRepository.GetByIdAsync(request.UserId, cancelToken);
            if(user is null)
            {
                return;
            }
            var employee = new Employee
            {
                UserId = request.UserId,
            };
            await _employeesRepository.UpdateAsync(employee, cancelToken);
        }
        public async Task DeleteAsync(int id, CancellationToken cancelToken)
        {
            await _employeesRepository.DeleteAsync(id, cancelToken);
        }
    }
}
