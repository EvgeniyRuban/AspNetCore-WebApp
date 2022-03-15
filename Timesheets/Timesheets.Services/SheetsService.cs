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

        public SheetsService(ISheetsRepository repository)
        {
            _sheetsRepository = repository;
        }
        public async Task AddAsync(SheetRequest sheet, CancellationToken cancelToken)
        {
            var newSheet = new Sheet
            {
                Date = sheet.Date,
                EmployeeId = sheet.EmployeeId,
                ContractId = sheet.ContractId,
                ServiceId = sheet.ServiceId,
                InvoiceId = sheet.InvoiceId,
                Amount = sheet.Amount,
            };
            await _sheetsRepository.AddAsync(newSheet, cancelToken);
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
        public async Task UpdateAsync(CreateSheetRequest newSheet, CancellationToken cancelToken)
        {
            var sheetToUpdate = await _sheetsRepository.GetAsync(newSheet.Id, cancelToken);
            if(sheetToUpdate is null)
            {
                return;
            }
            sheetToUpdate = new Sheet
            {
                Date = newSheet.Date,
                EmployeeId= newSheet.EmployeeId,
                ServiceId= newSheet.ServiceId,
                InvoiceId = newSheet.InvoiceId,
                Amount = newSheet.Amount,
            };
            await _sheetsRepository.UpdateAsync(sheetToUpdate, cancelToken);
        }
    }
}
