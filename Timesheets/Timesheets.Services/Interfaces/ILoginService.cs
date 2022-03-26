using System.Threading;
using System.Threading.Tasks;
using Timesheets.Entities;
using Timesheets.Entities.Dto;

namespace Timesheets.Services.Interfaces
{
    public interface ILoginService
    {
        Task<LoginResponse> AuthenticateAsync(LoginRequest request, CancellationToken cancelToken);
        Task<LoginResponse> RefreshTokenAsync(string tokenToRefresh, CancellationToken cancelToken);
        Task<UserResponse> RegisterAsync(CreateUserRequest request, CancellationToken cancelToken);
    }
}
