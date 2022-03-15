using System;
using System.Threading.Tasks;

namespace Timesheets.Services.Interfaces
{
    public interface IInvoicesService
    {
        Task<Guid> Create(InvoiceRequest request);
    }
}
