using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Timesheets.Entities;

namespace Timesheets.DataBase.Repositories
{
    public class EmployeesRepository : IDbRepository<Employee>
    {
        private AppDbContext _context;

        public EmployeesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Employee employee, CancellationToken token)
        {
            await _context.Employees.AddAsync(employee, token);
            await _context.SaveChangesAsync(token);
        }

        public async Task DeleteAsync(int id, CancellationToken token)
        {
            await Task.Run(() =>
            {
                var employeeToDelete = _context.Users
                                            .Where(e => e.Id == id)
                                            .FirstOrDefault();
                _context.Users.Remove(employeeToDelete);
            }, token);
            await _context.SaveChangesAsync(token);
        }

        public async Task<Employee> GetAsync(int id, CancellationToken token)
        {
            return await Task.Run(() =>
            {
                return _context.Employees
                                .Where(e => e.Id == id)
                                .FirstOrDefault();
            }, token);
        }

        public async Task<IReadOnlyCollection<Employee>> GetRangeAsync(int skip, int take, CancellationToken token)
        {
            return await Task.Run(() =>
            {
                return _context.Employees.Skip(skip).Take(take).ToList();
            }, token);
        }

        public async Task UpdateAsync(Employee newEmployee, CancellationToken token)
        {
            await Task.Run(() =>
            {
                var employee = _context.Employees
                                    .Where(u => u.Id == newEmployee.Id)
                                    .FirstOrDefault();
                if (employee == null)
                    return;

                employee.Id = newEmployee.Id;
                employee.FirstName = newEmployee.FirstName;
                employee.LastName = newEmployee.LastName;
                employee.Email = newEmployee.Email;
                employee.Age = newEmployee.Age;
            }, token);
            await _context.SaveChangesAsync(token);
        }
    }
}
