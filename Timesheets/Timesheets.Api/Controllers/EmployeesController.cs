using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Timesheets.Entities;
using Timesheets.Services.Interfaces;
using Timesheets.Entities.Dto;

namespace Timesheets.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesService _employeesService;

        public EmployeesController(IEmployeesService service)
        {
            _employeesService = service;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Employee>> GetAsync([FromRoute] int id, CancellationToken cancelToken)
        {
            var result = await _employeesService.GetAsync(id, cancelToken);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetRangeAsync(
            [FromQuery] int skip, 
            [FromQuery] int take, 
            CancellationToken cancelToken)
        {
            var result = await _employeesService.GetRangeAsync(skip, take, cancelToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> AddAsync([FromBody] CreateEmployeeRequest request, CancellationToken cancelToken)
        {
            await _employeesService.AddAsync(request, cancelToken);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAsync([FromBody] Employee employeeToUpdate, CancellationToken cancelToken)
        {
            await _employeesService.UpdateAsync(employeeToUpdate, cancelToken);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteAsync([FromRoute] int id, CancellationToken cancelToken)
        {
            await _employeesService.DeleteAsync(id, cancelToken);
            return Ok();
        }
    }
}
