using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Timesheets.Services;
using Timesheets.Entities;

namespace Timesheets.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEntityService<Employee> _service;

        public EmployeesController(IEntityService<Employee> service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Employee>> GetAsync([FromRoute] int id, CancellationToken token)
        {
            var result = await _service.GetAsync(id, token);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetRangeAsync(
            [FromQuery] int skip, 
            [FromQuery] int take, 
            CancellationToken token)
        {
            var result = await  _service.GetRangeAsync(skip, take, token);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> AddAsync([FromBody] Employee employee, CancellationToken token)
        {
            await _service.AddAsync(employee, token);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAsync([FromBody] Employee employee, CancellationToken token)
        {
            await _service.UpdateAsync(employee, token);
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
