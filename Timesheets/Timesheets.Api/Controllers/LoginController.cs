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
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<LoginResponse>> Authenticate([FromBody] LoginRequest request, CancellationToken cancelToken)
        {
            var token = await _loginService.AuthenticateAsync(request, cancelToken);
            if (token is null)
            {
                return BadRequest("Login or password is incorrect!");
            }
            SetTokenCookie(token.RefreshToken);
            return Ok(token);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshAsync(CancellationToken cancelToken)
        {
            var oldRefreshToken = Request.Cookies["refreshToken"];
            var newTokens = await _loginService.RefreshTokenAsync(oldRefreshToken, cancelToken);

            if (newTokens is null)
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
