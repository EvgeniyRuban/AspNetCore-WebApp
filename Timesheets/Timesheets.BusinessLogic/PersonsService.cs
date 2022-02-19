using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using Timesheets.Core.Services;
using Timesheets.Core.Repositories;
using Timesheets.Core.Models;

namespace Timesheets.BusinessLogic
{
    public class PersonsService : IPersonsService
    {
        private readonly IPersonsRepository _personsRepository;

        public PersonsService(IPersonsRepository personsRepository)
        {
            _personsRepository = personsRepository;
        }

        public async Task<Person> GetAsync(int id, CancellationToken token) => await _personsRepository.GetAsync(id, token); 

        public async Task<Person> GetAsync(string firstName, CancellationToken token) 
            => await _personsRepository.GetAsync(firstName, token);

        public async Task<IReadOnlyCollection<Person>> GetRangeAsync(int skip, int takeCount, CancellationToken token)
        => await _personsRepository.GetRangeAsync(skip, takeCount, token);

        public async Task AddAsync(Person person, CancellationToken token) => await _personsRepository.AddAsync(person, token);

        public async Task<bool> RemoveAsync(int id, CancellationToken token) => await _personsRepository.RemoveAsync(id, token);

        public async Task UpdateAsync(Person newPerson, CancellationToken token) 
            => await _personsRepository.UpdateAsync(newPerson, token);
    }
}
