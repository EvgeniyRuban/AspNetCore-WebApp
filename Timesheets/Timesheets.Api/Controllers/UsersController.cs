using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Timesheets.Entities;
using Timesheets.Services;

namespace Timesheets.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IEntityService<User> _userService;

        public UsersController(IEntityService<User> userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<User>> Get([FromRoute] Guid id, CancellationToken token)
        {
            return null;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetRangeAsync(
            [FromQuery] int skip, int take, CancellationToken token)
        {
            return null;
        }

        [HttpPost]
        public async Task<ActionResult> AddAsync([FromBody] User user, CancellationToken token)
        {
            return null;
        }

        [HttpPut]
        public async Task<ActionResult<bool>> UpdateAsync([FromBody] User user, CancellationToken token)
        {
            return null;
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<bool>> RemoveAsync([FromRoute] Guid id, CancellationToken token)
        {
            return null;
        }
    }
}
