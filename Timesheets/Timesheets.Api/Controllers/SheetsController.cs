using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Timesheets.Entities.Dto;
using Timesheets.Services.Interfaces;

namespace Timesheets.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class SheetsController : ControllerBase
    {
        private readonly ISheetsService _sheetsService;

        public SheetsController(ISheetsService service)
        {
            _sheetsService = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SheetResponse>> GetAsync([FromRoute] int id, CancellationToken cancelToken)
        {
            var response = await _sheetsService.GetAsync(id, cancelToken);
            if(response is null)
            {
                return NoContent();
            }
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SheetResponse>>> GetRangeAsync(
            [FromQuery] int skip, 
            [FromQuery]int take, 
            CancellationToken cancelToken)
        {
            var response = await _sheetsService.GetRangeAsync(skip, take, cancelToken);
            if(response is null)
            {
                return NoContent();
            }
            return Ok(response);
        }

        [HttpPost("create")]
        public async Task<ActionResult> AddAsync([FromBody] SheetRequest request, CancellationToken cancelToken)
        {
            await _sheetsService.AddAsync(request, cancelToken);
            return Ok();
        }

        [HttpPut("update")]
        public async Task<ActionResult> UpdateAsync(CreateSheetRequest request, CancellationToken cancelToken)
        {
            await _sheetsService.UpdateAsync(request,cancelToken);
            return Ok();
        }
    }
}
