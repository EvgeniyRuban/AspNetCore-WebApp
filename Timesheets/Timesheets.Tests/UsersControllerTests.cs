using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Timesheets.Api.Controllers;
using Timesheets.Entities.Dto;
using Timesheets.Services.Interfaces;
using Moq;
using Xunit;

namespace Timesheets.Tests
{
    public class UsersControllerTests
    {
        private readonly Mock<IUsersService> _usersService;

        public UsersControllerTests()
        {
            _usersService = new Mock<IUsersService>();
        }

        [Fact]
        public async void GetUserAsync_should_return_Ok()
        {
            var user = new UserResponse
            {
                Id = 1,
            };
            _usersService.Setup(e => e.GetByIdAsync(user.Id, default)).Returns(Task.Run(() => user));
            var usersController = new UsersController(_usersService.Object);

            var result = await usersController.GetAsync(user.Id, default);

            Assert.True(result.Result.GetType() == typeof(OkObjectResult));
        }
        [Fact]
        public async void GetUserAsync_should_return_NoContent()
        {
            var user = new UserResponse
            {
                Id = 2,
            };
            _usersService.Setup(e => e.GetByIdAsync(user.Id, default)).Returns(Task.Run(() => user));
            UsersController usersController = new UsersController(_usersService.Object);

            var result = await usersController.GetAsync(1, default);

            Assert.True(result.Result.GetType() == typeof(NoContentResult));
        }
    }
}
