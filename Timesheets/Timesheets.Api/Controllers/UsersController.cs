using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Timesheets.Entities.Dto;
using Timesheets.Services.Interfaces;

namespace Timesheets.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _userService;

        public UsersController(IUsersService usersService)
        {
            _userService = usersService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponse>> GetAsync([FromRoute] int id, CancellationToken cancelToken)
        {
            var userResponse = await _userService.GetByIdAsync(id, cancelToken);
            if(userResponse is null)
            {
                return NoContent();
            }
            return Ok(userResponse);
        }

        [AllowAnonymous]
        [HttpPost("create")]
        public async Task<ActionResult<UserResponse>> CreateAsync(CreateUserRequest request, CancellationToken cancelToken)
        {
            var response = await _userService.CreateAsync(request, cancelToken);
            return Ok(response);
        }
    }
}
