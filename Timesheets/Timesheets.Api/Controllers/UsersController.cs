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
        private readonly IEntityService<User> _service;

        public UsersController(IEntityService<User> service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<User>> GetAsync([FromRoute] int id, CancellationToken token)
        {
            var result = await _service.GetAsync(id, token);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetRangeAsync(
            [FromQuery] int skip, int take, CancellationToken token)
        {
            var result = await _service.GetRangeAsync(skip, take, token);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> AddAsync([FromBody] User user, CancellationToken token)
        {
            await _service.AddAsync(user, token);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAsync([FromBody] User user, CancellationToken token)
        {
            await _service.UpdateAsync(user, token);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteAsync([FromRoute] int id, CancellationToken token)
        {
            await _service.DeleteAsync(id, token);
            return Ok();
        }
    }
}
