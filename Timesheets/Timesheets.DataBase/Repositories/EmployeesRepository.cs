using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Timesheets.Entities;
using Timesheets.DataBase.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Timesheets.DataBase.Repositories
{
    public class EmployeeRepository : IEmployeesRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Employee> GetAsync(int id, CancellationToken cancelToken)
        {
            try
            {
                return await _context.Employees
                                        .FirstOrDefaultAsync(
                                            e => e.Id == id && e.IsDeleted == false, 
                                            cancelToken);
            }
            catch
            {
                return null;
            }
        }
        public async Task<IReadOnlyCollection<Employee>> GetRangeAsync(
            int skip,
            int take,
            CancellationToken cancelToken)
        {
            try
            {
                return await _context.Employees
                                        .Skip(skip)
                                        .Take(take)
                                        .ToListAsync(cancelToken);
            }
            catch
            {
                return null;
            }
        }
        public async Task AddAsync(Employee employee, CancellationToken cancelToken)
        {
            await _context.Employees.AddAsync(employee, cancelToken);
            await _context.SaveChangesAsync(cancelToken);
        }
        public async Task UpdateAsync(Employee newEmployee, CancellationToken cancelToken)
        {
            var employee = await _context.Employees
                                            .FirstOrDefaultAsync(
                                                e => e.Id == newEmployee.Id && e.IsDeleted == false, 
                                                cancelToken);
            if (employee != null)
            {
                employee.UserId = newEmployee.UserId;
                await _context.SaveChangesAsync(cancelToken);
            }
        }
        public async Task DeleteAsync(int id, CancellationToken cancelToken)
        {
            var employeeToDelete = await _context.Employees
                                                    .FirstOrDefaultAsync(
                                                        e => e.Id == id && e.IsDeleted == false, 
                                                        cancelToken);
            if (employeeToDelete != null)
            {
                employeeToDelete.IsDeleted = true;
                await _context.SaveChangesAsync(cancelToken);
            }
        }
    }
}
