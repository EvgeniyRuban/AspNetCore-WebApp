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
    public class InvoicesController : ControllerBase
    {
        private readonly IInvoicesService _invoicesService;

        public InvoicesController(IInvoicesService service)
        {
            _invoicesService = service;
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreateAsync([FromBody] InvoiceRequest request, CancellationToken cancelToken)
        {
            await _invoicesService.AddAsync(request, cancelToken);
            return Ok();
        }
    }
}
