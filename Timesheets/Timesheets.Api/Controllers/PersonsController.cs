using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Timesheets.Data.Models;
using Timesheets.BusinessLogic;

namespace Timesheets.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class PersonsController : ControllerBase
    {
        private readonly PersonsService _personsService;

        public PersonsController()
        {
            _personsService = new PersonsService();
        }

        [HttpGet()]
        [Route("{id}")]
        public async Task<ActionResult<Person>> GetAsync([FromRoute] int id, CancellationToken token)
        {
            var searchTask = Task.Run(() => _personsService.SearchAsync(id, token));

            searchTask.Wait();
            if (searchTask.Result == null)
            {
                return NoContent();
            }
            else
            {
                return Ok(searchTask.Result);
            }
        }

        [HttpGet]
        [Route("search")]
        public async Task<ActionResult<Person>> GetAsync ([FromQuery] string searchTerm, CancellationToken token)
        {
            var result = await Task.Run(() => _personsService.SearchAsync(searchTerm, token));
            if (result == null)
            {
                return NoContent();
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetRangeAsync(
            [FromQuery] int skip, 
            [FromQuery] int take, 
            CancellationToken token)
        {
            var result = await Task.Run(() => _personsService.GetRangeAsync(skip, take, token));
            if (result == null)
            {
                return NoContent();
            }
            else
            {
                return Ok(result);
            }
        }
            
        [HttpPost]
        public async Task<ActionResult> CreateAsync ([FromBody] Person person, CancellationToken token)
        {
            var result = await Task.Run(() => _personsService.CreateAsync(person, token));
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateByIdAsync ([FromBody] Person person, CancellationToken token)
        {
            await _personsService.UpdateByIdAsync(person.Id, person, token);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<bool>> Delete ([FromRoute] int id, CancellationToken token)
        {
            var result = await Task.Run(() => _personsService.DeleteAsync(id, token));
            return Ok(result);
        }
    }
}
