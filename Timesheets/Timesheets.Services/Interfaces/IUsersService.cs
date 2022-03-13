using System.Threading;
using System.Threading.Tasks;
using Timesheets.Entities;
using Timesheets.Entities.Dto;
using Timesheets.Entities.Dto.Authentication;

namespace Timesheets.Services.Interfaces
{
    public interface IUsersService
    {
        Task<User> GetByLoginAndPasswordAsync(LoginRequest loginRequest, CancellationToken cancelToken);
        Task<User> GetByRefreshToken(string refreshToken, CancellationToken cancelToken);
        Task<CreateUserResponse> CreateAsync(CreateUserRequest request, CancellationToken cancelToken);
        Task<LoginResponse> AuthenticateAsync(LoginRequest request, CancellationToken cancelToken);
        Task<LoginResponse> RefreshTokenAsync(string refreshToken, CancellationToken cancelToken);
    }
}
