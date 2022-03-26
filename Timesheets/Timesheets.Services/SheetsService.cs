using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Timesheets.DataBase.Repositories.Interfaces;
using Timesheets.Entities;
using Timesheets.Entities.Dto;
using Timesheets.Services.Interfaces;

namespace Timesheets.Services
{
    public sealed class SheetsService : ISheetsService
    {
        private readonly ISheetsRepository _sheetsRepository;
        private readonly IEmployeesRepository _employeesRepository;
        private readonly IContractsRepository _contractsRepository;
        private readonly IServicesRepository _servicesRepository;
        private readonly IInvoicesRepository _invoicesRepository;

        public SheetsService(
            ISheetsRepository sheetsRepository,
            IEmployeesRepository employeesRepository,
            IContractsRepository contractsRepository,
            IServicesRepository servicesRepository,
            IInvoicesRepository invoicesRepository)
        {
            _sheetsRepository = sheetsRepository;
            _employeesRepository = employeesRepository;
            _contractsRepository = contractsRepository;
            _servicesRepository = servicesRepository;
            _invoicesRepository = invoicesRepository;
        }

        public async Task<SheetResponse> CreateAsync(CreateSheetRequest request, CancellationToken cancelToken)
        {
            if(request == null)
            {
                return null;
            }

            var employee = await _employeesRepository.GetAsync(request.EmployeeId, cancelToken);
            var contract = await _contractsRepository.GetAsync(request.ContractId, cancelToken);
            var service = await _servicesRepository.GetAsync(request.ServiceId, cancelToken);
            var invoice = await _invoicesRepository.GetAsync(request.InvoiceId, cancelToken);

            if(employee is null ||
               contract is null ||
               service is null ||
               invoice is null)
            {
                return null;
            }
            var sheet = new Sheet
            {
                Date = request.Date,
                EmployeeId = request.EmployeeId,
                ContractId = request.ContractId,
                ServiceId = request.ServiceId,
                InvoiceId = request.InvoiceId,
                Amount = request.Amount,
            };
            var newSheet = await _sheetsRepository.CreateAsync(sheet, cancelToken);

            if(newSheet != null)
            {
                return new SheetResponse
                {
                    Id = newSheet.Id,
                    EmployeeId = newSheet.EmployeeId,
                    ContractId = newSheet.ContractId,
                    ServiceId = newSheet.ServiceId,
                    InvoiceId = newSheet.InvoiceId,
                    Amount = newSheet.Amount,
                };
            }
            return null;
        }
        public async Task<SheetResponse> GetAsync(int id, CancellationToken cancelToken)
        {
            var sheet = await _sheetsRepository.GetAsync(id, cancelToken);  
            if (sheet is null)
            { 
                return null; 
            }
            return new SheetResponse
            {
                Id = sheet.Id,
                EmployeeId= sheet.EmployeeId,
                ContractId = sheet.ContractId,
                ServiceId= sheet.ServiceId,
                InvoiceId= sheet.InvoiceId,
                Amount= sheet.Amount,
            };
        }
        public async Task<IReadOnlyCollection<SheetResponse>> GetRangeAsync(int skip, int take, CancellationToken cancelToken)
        {
            var sheets = await _sheetsRepository.GetRangeAsync(skip, take, cancelToken);
            if(sheets is null)
            {
                return null;
            }
            var sheetsCollection = new List<SheetResponse>(sheets.Count);
            foreach(var item in sheets)
            {
                sheetsCollection.Add(new SheetResponse
                {
                    Id = item.Id,
                    EmployeeId = item.EmployeeId,
                    ContractId = item.ContractId,
                    ServiceId = item.ServiceId,
                    InvoiceId = item.InvoiceId,
                    Amount = item.Amount,
                });
            }
            return sheetsCollection;
        }
        public async Task UpdateAsync(SheetRequest request, CancellationToken cancelToken)
        {
            if (request == null)
            {
                return;
            }

            var employee = await _employeesRepository.GetAsync(request.EmployeeId, cancelToken);
            var contract = await _contractsRepository.GetAsync(request.ContractId, cancelToken);
            var service = await _servicesRepository.GetAsync(request.ServiceId, cancelToken);
            var invoice = await _invoicesRepository.GetAsync(request.InvoiceId, cancelToken);

            if (employee is null ||
               contract is null ||
               service is null ||
               invoice is null)
            {
                return;
            }

            var sheetToUpdate = await _sheetsRepository.GetAsync(request.Id, cancelToken);

            if(sheetToUpdate is null)
            {
                return;
            }

            sheetToUpdate = new Sheet
            {
                Date = request.Date,
                EmployeeId = request.EmployeeId,
                ServiceId = request.ServiceId,
                InvoiceId = request.InvoiceId,
                Amount = (int)request.Amount,
            };
            await _sheetsRepository.UpdateAsync(sheetToUpdate, cancelToken);
        }
    }
}
