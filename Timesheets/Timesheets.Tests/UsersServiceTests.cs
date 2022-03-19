using Moq;
using System.Threading.Tasks;
using Timesheets.DataBase.Repositories.Interfaces;
using Timesheets.Entities;
using Timesheets.Entities.Dto;
using Timesheets.Services;
using Timesheets.Services.Interfaces;
using Xunit;

namespace Timesheets.Tests
{
    public class UsersServiceTests
    {
        private readonly Mock<IUsersRepository> _usersRepository;
        private readonly Mock<ILoginService> _loginService;

        public UsersServiceTests()
        {
            _loginService = new Mock<ILoginService>();
            _usersRepository = new Mock<IUsersRepository>();
        }

        [Fact]
        public async void GetByIdAsync_should_return_notnull()
        {
            var user = new User
            {
                Id = 1,
            };
            _usersRepository.Setup(x => x.GetByIdAsync(user.Id, default)).Returns(Task.Run(() => user));
            var usersService = new UsersService(_usersRepository.Object, _loginService.Object);

            var result = await usersService.GetByIdAsync(user.Id, default);

            Assert.NotNull(result);
        }
        [Fact]
        public async void GetByIdAsync_should_return_null()
        {
            var user = new User
            {
                Id = 1,
                Name = "test"
            };
            _usersRepository.Setup(x => x.GetByIdAsync(user.Id, default)).ReturnsAsync(user);
            var usersService = new UsersService(_usersRepository.Object, _loginService.Object);

            var result = await usersService.GetByIdAsync(2, default);

            Assert.Null(result);
        }
        [Fact]
        public async void GetModelByIdAsync_should_return_notnull()
        {
            var user = new User
            {
                Id = 1,
                Name = "test"
            };
            _usersRepository.Setup(x => x.GetByIdAsync(user.Id, default)).ReturnsAsync(user);
            var usersService = new UsersService(_usersRepository.Object, _loginService.Object);

            var result = await usersService.GetModelByIdAsync(1, default);

            Assert.NotNull(result);
        }
        [Fact]
        public async void GetModelByIdAsync_should_return_null()
        {
            var user = new User
            {
                Id = 1,
                Name = "test"
            };
            _usersRepository.Setup(x => x.GetByIdAsync(user.Id, default)).ReturnsAsync(user);
            var usersService = new UsersService(_usersRepository.Object, _loginService.Object);

            var result = await usersService.GetModelByIdAsync(2, default);

            Assert.Null(result);
        }
        [Fact]
        public async void CreateAsync_should_return_notnull()
        {
            var createUserRequest = new CreateUserRequest
            {
                Name = "test",
                Surname = "test",
                Login = "test",
                Password = "test"
            };
            var userResponse = new UserResponse
            {
                Id = 1,
                Name = createUserRequest.Name,
                Surname = createUserRequest.Surname,
            };
            _loginService.Setup(x => x.RegisterAsync(createUserRequest, default)).ReturnsAsync(userResponse);
            var usersService = new UsersService(_usersRepository.Object, _loginService.Object);

            var result = await usersService.CreateAsync(createUserRequest, default);

            Assert.NotNull(result);
        }
        [Fact]
        public async void CreateAsync_should_return_null()
        {
            var usersService = new UsersService(_usersRepository.Object, _loginService.Object);

            var result = await usersService.CreateAsync(null, default);

            Assert.Null(result);
        }
    }
}
