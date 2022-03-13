using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Timesheets.Entities.Dto;
using Timesheets.Entities.Dto.Authentication;
using Timesheets.Services.Interfaces;

namespace Timesheets.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [Authorize]
        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<CreateUserResponse>> CreateAsync(CreateUserRequest request, CancellationToken cancelToken)
        {
            var userResponse = await _usersService.CreateAsync(request, cancelToken);
            return Ok(userResponse);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public async Task<ActionResult<LoginResponse>> Authenticate([FromBody] LoginRequest request, CancellationToken cancelToken)
        {
            var token = await _usersService.AuthenticateAsync(request, cancelToken);
            if (token is null)
            {
                return BadRequest("Login or password is incorrect!");
            }
            SetTokenCookie(token.RefreshToken);
            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("refresh-token")]
        public async Task<ActionResult<string>> Refresh(CancellationToken cancelToken)
        {
            var oldRefreshToken = Request.Cookies["refreshToken"];
            var newTokens = await _usersService.RefreshTokenAsync(oldRefreshToken, cancelToken);

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
