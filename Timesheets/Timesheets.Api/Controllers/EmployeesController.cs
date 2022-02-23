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
        private readonly IEntityService<Employee> _employeesService;

        public EmployeesController(IEntityService<Employee> employeesService)
        {
            _employeesService = employeesService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Employee>> Get([FromRoute] Guid id, CancellationToken token)
        {
            return null;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetRangeAsync(
            [FromQuery] int skip, 
            [FromQuery] int take, 
            CancellationToken token)
        {
            return null;
        }

        [HttpPost]
        public async Task<ActionResult> AddAsync([FromBody] Employee employee, CancellationToken token)
        {
            return null;
        }

        [HttpPut]
        public async Task<ActionResult<bool>> UpdateAsync([FromBody] Employee employee, CancellationToken token)
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
