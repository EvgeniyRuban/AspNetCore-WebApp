using System;
using System.Threading.Tasks;
using Timesheets.Services.Interfaces;

namespace Timesheets.Services
{
    public sealed class InvoicesService : IInvoicesService
    {
        public Task<Guid> Create(InvoiceRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
