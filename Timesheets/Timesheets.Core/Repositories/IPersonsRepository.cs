using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Timesheets.Core.Models;

namespace Timesheets.Core.Repositories
{
    public interface IPersonsRepository
    {
        Task<Person> GetAsync(int id, CancellationToken token);
        Task<Person> GetAsync(Person term, CancellationToken token);
        Task<IReadOnlyCollection<Person>> GetRangeAsync(int skip, int takeCount, CancellationToken token);
        Task<bool> AddAsync(Person person, CancellationToken token);
        Task<bool> TryUpdateAsync(Person newPerson, CancellationToken token);
        Task<bool> RemoveAsync(int id, CancellationToken token);
    }
}
