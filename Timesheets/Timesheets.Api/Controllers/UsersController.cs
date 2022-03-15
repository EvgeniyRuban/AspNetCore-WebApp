using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Timesheets.Entities.Dto;
using Timesheets.Services.Interfaces;

namespace Timesheets.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _userService;

        public UsersController(IUsersService usersService)
        {
            _userService = usersService;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponse>> GetAsync([FromRoute] Guid id, CancellationToken cancelToken)
        {
            var userResponse = await _userService.GetByIdAsync(id, cancelToken);
            if(userResponse is null)
            {
                return NotFound(id);
            }
            return Ok(userResponse);
        }

        [AllowAnonymous]
        [HttpPost("create")]
        public async Task<ActionResult<UserResponse>> CreateAsync(CreateUserRequest request, CancellationToken cancelToken)
        {
            var userResponse = await _userService.CreateAsync(request, cancelToken);
            return Ok(userResponse);
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<ActionResult<LoginResponse>> Authenticate([FromBody] LoginRequest request, CancellationToken cancelToken)
        {
            var token = await _userService.AuthenticateAsync(request, cancelToken);
            if (token is null)
            {
                return BadRequest("Login or password is incorrect!");
            }
            SetTokenCookie(token.RefreshToken);
            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> Refresh(CancellationToken cancelToken)
        {
            var oldRefreshToken = Request.Cookies["refreshToken"];
            var newTokens = await _userService.RefreshTokenAsync(oldRefreshToken, cancelToken);

            if (string.IsNullOrWhiteSpace(newTokens.AccessToken) ||
                string.IsNullOrWhiteSpace(newTokens.RefreshToken))
            {
                return Unauthorized("Invalid token.");
            }

            SetTokenCookie(newTokens.RefreshToken);
            return Ok(newTokens);
        }

        private void SetTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }
    }
}
