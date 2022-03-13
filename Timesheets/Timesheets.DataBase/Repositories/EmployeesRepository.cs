using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Timesheets.Entities;
using Timesheets.DataBase.Repositories.Interfaces;

namespace Timesheets.DataBase.Repositories
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly AppDbContext _context;

        public EmployeesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Employee> GetAsync(int id, CancellationToken cancelToken)
        {
            return await Task.Run(() =>
            {
                try
                {
                    return _context.Employees
                                    .Where(e => e.Id == id && e.IsDeleted == false)
                                    .FirstOrDefault();
                }
                catch
                {
                    return null;
                }
            }, cancelToken);
        }
        public async Task<IReadOnlyCollection<Employee>> GetRangeAsync(
            int skip, 
            int take, 
            CancellationToken cancelToken)
        {
            return await Task.Run(() =>
            {
                try
                {
                    return _context.Employees.Skip(skip).Take(take).ToList();
                }
                catch
                {
                    return null;
                }
            }, cancelToken);
        }
        public async Task AddAsync(Employee employee, CancellationToken cancelToken)
        {
            await _context.Employees.AddAsync(employee, cancelToken);
            await _context.SaveChangesAsync(cancelToken);
        }
        public async Task UpdateAsync(Employee newEmployee, CancellationToken cancelToken)
        {
            var employee = await Task.Run(() =>
            {
                return _context.Employees
                                .Where(u => u.Id == newEmployee.Id && u.IsDeleted == false)
                                .FirstOrDefault();
            }, cancelToken);
            if (employee != null)
            {
                employee.UserId = newEmployee.UserId;
                employee.IsDeleted = newEmployee.IsDeleted;
            }
            await _context.SaveChangesAsync(cancelToken);
        }
        public async Task DeleteAsync(int id, CancellationToken cancelToken)
        {
            var employeeToDelete = await Task.Run(() =>
            {
                return _context.Employees
                                .Where(e => e.Id == id && e.IsDeleted == false)
                                .FirstOrDefault();
            }, cancelToken);

            if (employeeToDelete != null)
            {
                employeeToDelete.IsDeleted = true;
            }

            await _context.SaveChangesAsync(cancelToken);
        }
    }
}
