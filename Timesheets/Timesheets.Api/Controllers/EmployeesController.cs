using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Timesheets.Services.Interfaces;
using Timesheets.Entities.Dto;


namespace Timesheets.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesService _employeeService;

        public EmployeesController(IEmployeesService service)
        {
            _employeeService = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeResponse>> GetAsync([FromRoute] int id, CancellationToken cancelToken)
        {
            var result = await _employeeService.GetAsync(id, cancelToken);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeResponse>>> GetRangeAsync(
            [FromQuery] int skip, 
            [FromQuery] int take, 
            CancellationToken cancelToken)
        {
            var result = await _employeeService.GetRangeAsync(skip, take, cancelToken);
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<ActionResult> AddAsync([FromBody] CreateEmployeeRequest request, CancellationToken cancelToken)
        {
            await _employeeService.AddAsync(request, cancelToken);
            return Ok();
        }

        [HttpPut("update")]
        public async Task<ActionResult> UpdateAsync([FromBody] EmployeeRequest employeeToUpdate, CancellationToken cancelToken)
        {
            await _employeeService.UpdateAsync(employeeToUpdate, cancelToken);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync([FromRoute] int id, CancellationToken cancelToken)
        {
            await _employeeService.DeleteAsync(id, cancelToken);
            return Ok();
        }
    }
}
