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
        public async Task<ActionResult<InvoiceResponse>> CreateAsync([FromBody] InvoiceRequest request, CancellationToken cancelToken)
        {
            var response = await _invoicesService.CreateAsync(request, cancelToken);
            return Ok(response);
        }
    }
}
