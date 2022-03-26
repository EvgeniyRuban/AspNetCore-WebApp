using System.Threading;
using System.Threading.Tasks;
using Timesheets.Entities;

namespace Timesheets.DataBase.Repositories.Interfaces
{
    /// <summary>
    /// Methods for employees data managnent at the database level.
    /// </summary>
    public interface IEmployeesRepository : IBaseRepository<Employee>
    {
        Task DeleteAsync(int id, CancellationToken cancelToken);
    }
}
