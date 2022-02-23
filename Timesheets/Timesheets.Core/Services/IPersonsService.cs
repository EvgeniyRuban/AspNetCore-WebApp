using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Timesheets.Core.Models;

namespace Timesheets.Core.Services
{
    public interface IPersonsService
    {
        Task<Person> GetAsync(int id, CancellationToken token);
        Task<Person> GetAsync(Person term, CancellationToken token);
        Task<IReadOnlyCollection<Person>> GetRangeAsync(int skip, int takeCount, CancellationToken token);
        Task AddAsync(Person person, CancellationToken token);
        Task UpdateAsync(Person newPerson, CancellationToken token);
        Task<bool> RemoveAsync(int id, CancellationToken token);
    }
}
