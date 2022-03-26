using System.Threading;
using System.Threading.Tasks;
using Timesheets.Entities;
using Timesheets.Services.Interfaces;
using Timesheets.Entities.Dto;
using Timesheets.DataBase.Repositories.Interfaces;

namespace Timesheets.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly ILoginService _loginService;

        public UsersService(IUsersRepository usersRepository, ILoginService loginService)
        {
            _usersRepository = usersRepository;
            _loginService = loginService;
        }

        public async Task<UserResponse> GetByIdAsync(int id, CancellationToken cancelToken)
        {
            var user = await _usersRepository.GetByIdAsync(id, cancelToken);
            if(user is null)
            {
                return null;
            }
            return new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Age = user.Age,
            };
        }
        public async Task<User> GetModelByIdAsync(int id, CancellationToken cancelToken)
        {
            return await _usersRepository.GetByIdAsync(id, cancelToken);
        }
        public async Task<UserResponse> CreateAsync(CreateUserRequest request, CancellationToken cancelToken)
        {
            return await _loginService.RegisterAsync(request, cancelToken);
        }
    }
}