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

        public async Task<User> GetByLoginAndPasswordAsync(
            string login, 
            string password, 
            CancellationToken token)
        {
            return await Task.Run(() =>
            {
                try
                {
                    return _context.Users
                                    .Where(u => u.UserName == login && u.PasswordHash == password)
                                    .FirstOrDefault();
                }
                catch
                {
                    return null;
                }
            }, token);
        }
        public async Task<User> GetByLoginAsync(string login, CancellationToken cancelToken)
        {
            return await Task.Run(() =>
            {
                return _context.Users
                            .Where(u => u.Login == login)
                            .FirstOrDefault();
            }, cancelToken);
        }
        public Task<User> GetByRefreshToken(string refreshToken, CancellationToken cancelToken)
        {
            return Task.Run(() =>
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
                        .Where(u => u.Id == userToUpdate.Id)
                        .FirstOrDefault();
            }, cancelToken);

            user = new User
            {
                Id = userToUpdate.Id,
                UserName = userToUpdate.UserName,
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
