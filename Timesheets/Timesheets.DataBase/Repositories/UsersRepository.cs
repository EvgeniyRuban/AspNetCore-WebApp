using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Timesheets.Entities;
using Timesheets.DataBase.Repositories.Interfaces;

namespace Timesheets.DataBase.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly AppDbContext _context;

        public UsersRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetByIdAsync(Guid id, CancellationToken cancelToken)
        {
            return await Task.Run(() => 
                                _context.Users.FirstOrDefault(u => u.Id == id), 
                                cancelToken);
        }
        public async Task<User> GetByLoginAsync(string login, CancellationToken cancelToken)
        {
            return await Task.Run(() => 
                                _context.Users.FirstOrDefault(u => u.Login == login), 
                                cancelToken);
        }
        public async Task<User> GetByRefreshToken(string refreshToken, CancellationToken cancelToken)
        {
            return await Task.Run(() =>
            {
                try
                {
                    return _context.Users
                                    .Where(u => u.RefreshToken == refreshToken)
                                    .FirstOrDefault();
                }
                catch
                {
                    return null;
                }
            }, cancelToken);
        }
        public async Task UpdateByIdAsync(User userToUpdate, CancellationToken cancelToken)
        {
            var user = await Task.Run(() =>
            {

                return _context.Users
                        .FirstOrDefault( u => u.Id == userToUpdate.Id);
            }, cancelToken);

            user = new User
            {
                Id = userToUpdate.Id,
                Name = userToUpdate.Name,
                Surname = userToUpdate.Surname,
                Age = userToUpdate.Age,
                Login = userToUpdate.Login,
                PasswordHash = userToUpdate.PasswordHash,
                PasswordSalt = userToUpdate.PasswordSalt,
                RefreshToken = userToUpdate.RefreshToken,
            };
            await _context.SaveChangesAsync(cancelToken);
        }
        public async Task AddAsync(User user, CancellationToken cancelToken)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync(cancelToken);
        }
    }
}
