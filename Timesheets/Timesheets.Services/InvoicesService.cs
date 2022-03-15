using System.Threading;
using System.Threading.Tasks;
using Timesheets.DataBase.Repositories.Interfaces;
using Timesheets.Entities;
using Timesheets.Entities.Dto;
using Timesheets.Services.Interfaces;

namespace Timesheets.Services
{
    public sealed class InvoicesService : IInvoicesService
    {
        private readonly IInvoicesRepository _invoiceRepository;

        public InvoicesService(IInvoicesRepository repository)
        {
            _invoiceRepository = repository;
        }

        public async Task AddAsync(InvoiceRequest request, CancellationToken cancelToken)
        {
            var newInvoice = new Invoice
            {
                ContractId = request.ContractId,
                DateStart = request.DateStart,
                DateEnd = request.DateEnd,
                Sum = request.Sum,
            };
            await _invoiceRepository.AddAsync(newInvoice, cancelToken);
        }
    }
}
