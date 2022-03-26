using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Timesheets.Entities.Dto;
using Timesheets.Services.Interfaces;

namespace Timesheets.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContractsController : ControllerBase
    {
        private readonly IContractsService _contractsService;

        public ContractsController(IContractsService contractsService)
        {
            _contractsService = contractsService;
        }

        [HttpPost("create")]
        public async Task<ActionResult<ContractResponse>> CreateAsync([FromBody] ContractRequest contract, CancellationToken cancelToken)
        {
            var response = await _contractsService.CreateAsync(contract, cancelToken);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContractResponse>> GetAsync([FromRoute] int id, CancellationToken cancelToken)
        {
            var response = await _contractsService.GetAsync(id, cancelToken);
            if(response is null)
            {
                return NoContent();
            }
            return Ok(response);
        }

        [HttpGet("{id}/is-active")]
        public async Task<ActionResult<bool>> IsActive([FromRoute] int id, CancellationToken cancelToken)
        {
            var response = await _contractsService.CheckContractIsActiveAsync(id, cancelToken);
            if(response is null)
            {
                return NoContent();
            }
            return Ok(response);
        }

        [HttpDelete("{id}/delete")]
        public async Task<ActionResult> DeleteAsync([FromRoute] int id, CancellationToken cancelToken)
        {
            await _contractsService.DeleteAsync(id,cancelToken);
            return Ok();
        }
    }
}
