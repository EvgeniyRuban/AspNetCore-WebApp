using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Timesheets.Api.Models;

namespace Timesheets.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonsController : ControllerBase
    {
        [HttpGet]
        [Route("/{id}")]
        public Task<ActionResult<Person>> Get ([FromRoute] int id, CancellationToken token)
        {
            return null;
        }

        [HttpGet]
        [Route("/search")]
        public Task<ActionResult<Person>> Get ([FromQuery] string searchTerm, CancellationToken token)
        {
            return null;
        }

        [HttpGet]
        public Task<ActionResult<IEnumerable<Person>>> GetRange (
            [FromQuery] int skip, 
            [FromQuery] int take, 
            CancellationToken token)
        {
            return null;
        }
            
        [HttpPost]
        public Task<ActionResult> Add ([FromBody] Person person, CancellationToken token)
        {
            return null;
        }

        [HttpPut]
        public Task<ActionResult> Update ([FromBody] Person person, CancellationToken token)
        {
            return null;
        }

        [HttpDelete]
        [Route("/{id}")]
        public Task<ActionResult> Delete ([FromRoute] int id, CancellationToken token)
        {
            return null;
        }
    }
}
