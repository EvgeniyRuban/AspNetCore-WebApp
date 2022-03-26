using System.Threading;
using System.Threading.Tasks;
using Timesheets.Entities.Dto;

namespace Timesheets.Services.Interfaces
{
    public interface IInvoicesService
    {
        Task<InvoiceResponse> GetAsync(int id, CancellationToken cancelToken);
        Task<InvoiceResponse> CreateAsync(InvoiceRequest request, CancellationToken cancelToken);
    }
}
