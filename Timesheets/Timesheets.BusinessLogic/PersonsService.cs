using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Timesheets.Data;
using Timesheets.Data.Models;

namespace Timesheets.BusinessLogic
{
    public class PersonsService
    {
        public async Task<IEnumerable<Person>> GetRangeAsync(int startIndex, int count, CancellationToken token)
        {
            return await Task.Run(() =>
            {
                if(token.IsCancellationRequested)
                {
                    return null;
                }
                return Repository.Persons.GetRange(startIndex, count);
            });
        }
        public async Task<Person> CreateAsync(Person person, CancellationToken token)
        {
            return await Task.Run(() => 
            {
                if(token.IsCancellationRequested)
                {
                    return null;
                }
                Repository.Persons.Add(person);
                return person;
            });
        }
        public async Task<Person> SearchAsync(int id, CancellationToken token)
        {
            return await Task.Run(() =>
            {
                foreach (var person in Repository.Persons)
                {
                    if (token.IsCancellationRequested)
                    {
                        break;
                    }
                    if (person.Id == id)
                    {
                        return person;
                    }
                }

                return null;
            });
        }
        public async Task<Person> SearchAsync(string name, CancellationToken token)
        {
            return await Task.Run(() =>
            {
                foreach (var person in Repository.Persons)
                {
                    if (token.IsCancellationRequested)
                    {
                        break;
                    }
                    if (person.FirstName == name)
                    {
                        return person;
                    }
                }

                return null;
            });
        }
        public async Task UpdateByIdAsync(int id, Person newPerson, CancellationToken token)
        {
            var person = await SearchAsync(id, token);
            person = newPerson;
        }
        public async Task<bool> DeleteAsync(int id, CancellationToken token)
        {
            return Repository.Persons.Remove(await SearchAsync(id, token));
        }
    }
}
