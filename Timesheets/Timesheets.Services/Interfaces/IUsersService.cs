using System.Threading;
using System.Threading.Tasks;
using Timesheets.Entities;
using Timesheets.Entities.Dto;

namespace Timesheets.Services.Interfaces
{
    /// <summary>
    /// Methods for users repository managnent and authentication at the business logic level.
    /// </summary>
    public interface IUsersService
    {
        Task<UserResponse> GetByIdAsync(int id, CancellationToken cancelToken);
        Task<UserResponse> CreateAsync(CreateUserRequest request, CancellationToken cancelToken);
        Task<User> GetModelByIdAsync(int id, CancellationToken cancelToken);
        
    }
}
