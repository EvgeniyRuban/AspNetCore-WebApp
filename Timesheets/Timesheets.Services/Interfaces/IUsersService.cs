using System;
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
        Task<UserResponse> GetByIdAsync(Guid id, CancellationToken cancelToken);
        Task<User> GetModelByIdAsync(Guid id, CancellationToken cancelToken);
        Task<User> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancelToken);
        Task<UserResponse> CreateAsync(CreateUserRequest request, CancellationToken cancelToken);
        Task<LoginResponse> AuthenticateAsync(LoginRequest request, CancellationToken cancelToken);
        Task<LoginResponse> RefreshTokenAsync(string refreshToken, CancellationToken cancelToken);
    }
}
