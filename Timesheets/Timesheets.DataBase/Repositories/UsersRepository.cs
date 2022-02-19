using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Timesheets.Entities;

namespace Timesheets.DataBase.Repositories
{
    public class UsersRepository : IDbRepository<User>
    {
        private AppDbContext _context;

        public UsersRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user, CancellationToken token)
        {
            await _context.Users.AddAsync(user, token);
            await _context.SaveChangesAsync(token);
        }

        public async Task DeleteAsync(int id, CancellationToken token)
        {
            await Task.Run(() =>
            {
                var userToDelete = _context.Users
                                            .Where(u => u.Id == id)
                                            .FirstOrDefault();
                _context.Users.Remove(userToDelete);
            }, token);
            await _context.SaveChangesAsync(token);
        }

        public async Task<User> GetAsync(int id, CancellationToken token)
        {
            return await Task.Run(() =>
            {
                return _context.Users
                                .Where(u => u.Id == id)
                                .FirstOrDefault();
            }, token);
        }

        public async Task<IReadOnlyCollection<User>> GetRangeAsync(int skip, int take, CancellationToken token)
        {
            return await Task.Run(() =>
            {
                return _context.Users.Skip(skip).Take(take).ToList();
            }, token);
        }

        public async Task UpdateAsync(User newUser, CancellationToken token)
        {
            await Task.Run(() =>
            {
                var user = _context.Users
                                    .Where(u => u.Id == newUser.Id)
                                    .FirstOrDefault();
                if (user == null)
                    return;

                user.FirstName = newUser.FirstName;
                user.LastName = newUser.LastName;
                user.Email = newUser.Email;
                user.Age = newUser.Age;
            }, token);
            await _context.SaveChangesAsync(token);
        }
    }
}
