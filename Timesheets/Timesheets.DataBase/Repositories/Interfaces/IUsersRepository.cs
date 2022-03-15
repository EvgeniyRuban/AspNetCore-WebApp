using System.Threading;
using System.Threading.Tasks;
using Timesheets.Entities;

namespace Timesheets.DataBase.Repositories.Interfaces
{
    /// <summary>
    /// Methods for users data managnent at the database level.
    /// </summary>
    public interface IUsersRepository
    {
        Task<User> GetByIdAsync(int id, CancellationToken cancelToken);
        Task<User> GetByLoginAsync(string login, CancellationToken cancelToken);
        Task<User> GetByRefreshToken(string refreshToken, CancellationToken cancelToken);
        Task UpdateByIdAsync(User userToUpdate, CancellationToken cancelToken);
        Task AddAsync(User user, CancellationToken cancelToken);
    }
}
