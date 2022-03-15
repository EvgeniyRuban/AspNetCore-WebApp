using System;
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
        private readonly IUsersService _usersService;

        public EmployeesService(IEmployeesRepository employeesRepository, IUsersService usersService)
        {
            _employeesRepository = employeesRepository;
            _usersService = usersService;
        }

        public async Task<EmployeeResponse> GetAsync(Guid id, CancellationToken cancelToken)
        {
            var employee = await _employeesRepository.GetAsync(id, cancelToken);
            if (employee is null)
            {
                return null;
            }
            return new EmployeeResponse
            {
                Id = employee.Id,
                User = await _usersService.GetByIdAsync((Guid)employee.UserId, cancelToken),
            };
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
        public async Task AddAsync(CreateEmployeeRequest request, CancellationToken cancelToken)
        {
            var employee = new Employee
            {
                UserId = request.UserId,
            };
            if(employee.UserId != null)
            {
                employee.User = await _usersService.GetModelByIdAsync((Guid)request.UserId, cancelToken);
            }
            await _employeesRepository.AddAsync(employee, cancelToken);
        }
        public async Task UpdateAsync(EmployeeRequest request, CancellationToken cancelToken)
        {
            var user = await _usersService.GetByIdAsync((Guid)request.UserId, cancelToken);
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
        public async Task DeleteAsync(Guid id, CancellationToken cancelToken)
        {
            await _employeesRepository.DeleteAsync(id, cancelToken);
        }
    }
}
