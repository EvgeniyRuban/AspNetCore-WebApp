using System.Threading;
using System.Threading.Tasks;
using Timesheets.Entities.Dto;

namespace Timesheets.Services.Interfaces
{
    public interface IInvoicesService
    {
        Task AddAsync(InvoiceRequest request, CancellationToken cancelToken);
    }
}
