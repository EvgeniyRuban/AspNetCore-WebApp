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
        private readonly IContractsRepository _contractsRepository;

        public InvoicesService(IInvoicesRepository invoiceRepository, IContractsRepository contractsRepository)
        {
            _invoiceRepository = invoiceRepository;
            _contractsRepository = contractsRepository;
        }

        public async Task<InvoiceResponse> GetAsync(int id, CancellationToken cancelToken)
        {
            var invoiceResponse = await _invoiceRepository.GetAsync(id, cancelToken);
            if (invoiceResponse is null)
            {
                return null;
            }
            return new InvoiceResponse
            {
                Id = invoiceResponse.Id,
                ContractId = invoiceResponse.ContractId,
                DateStart = invoiceResponse.DateStart,
                DateEnd = invoiceResponse.DateEnd,
                Sum = invoiceResponse.Sum,
            };
        }
        public async Task<InvoiceResponse> CreateAsync(InvoiceRequest request, CancellationToken cancelToken)
        {
            if(request is null)
            {
                return null;
            }
            var contract = await _contractsRepository.GetAsync(request.ContractId, cancelToken);
            if(contract is null)
            {
                return null;
            }
            var invoice = new Invoice
            {
                ContractId = request.ContractId,
                DateStart = request.DateStart,
                DateEnd = request.DateEnd,
                Sum = request.Sum,
            };
            var newInvoice =  await _invoiceRepository.CreateAsync(invoice, cancelToken);
            if(newInvoice != null)
            {
                return new InvoiceResponse
                {
                    Id = newInvoice.Id,
                    ContractId = newInvoice.ContractId,
                    DateStart = newInvoice.DateStart,
                    DateEnd= newInvoice.DateEnd,
                    Sum = newInvoice.Sum,
                };
            }
            return null;
        }
        
    }
}
