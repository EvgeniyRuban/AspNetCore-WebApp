using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Timesheets.DataBase.Repositories.Interfaces;
using Timesheets.Entities;

namespace Timesheets.DataBase.Repositories
{
    public sealed class SheetsRepository : ISheetsRepository
    {
        private readonly AppDbContext _context;

        public SheetsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Sheet> GetAsync(int id, CancellationToken cancelToken)
        {
            return await _context.Sheets.FirstOrDefaultAsync(i => i.Id == id, cancelToken);
        }
        public async Task<IReadOnlyCollection<Sheet>> GetRangeAsync(int skip, int take, CancellationToken cancelToken)
        {
            try
            {
                return await _context.Sheets
                                        .Skip(skip)
                                        .Take(take)
                                        .ToListAsync(cancelToken);
            }
            catch
            {
                return null;
            }
        }
        public async Task<Sheet> CreateAsync(Sheet item, CancellationToken cancelToken)
        {
            var entityEntry = await _context.Sheets.AddAsync(item, cancelToken);
            await _context.SaveChangesAsync(cancelToken);
            return entityEntry.Entity;
        }
        public async Task UpdateAsync(Sheet item, CancellationToken cancelToken)
        {
            var updateItem = await GetAsync(item.Id, cancelToken);
            if (updateItem is null)
            {
                updateItem = new Sheet
                {
                    Date = item.Date,
                    EmployeeId = item.EmployeeId,
                    ContractId = item.ContractId,
                    ServiceId = item.ServiceId,
                    InvoiceId = item.InvoiceId,
                    Amount = item.Amount,
                    IsApproved = item.IsApproved,
                    ApprovedDate = item.ApprovedDate,
                    Employee = item.Employee,
                    Contract = item.Contract,
                    Service = item.Service,
                    Invoice = item.Invoice,
                };
                await _context.SaveChangesAsync(cancelToken);
            }
        }
        public async Task<IEnumerable<Sheet>> GetItemsForInvoice(
            int contractId, 
            DateTime dateStart, 
            DateTime dateEnd,
            CancellationToken cancelToken)
        {
            var sheets = await _context.Sheets
                                        .Where(x => x.ContractId == contractId)
                                        .Where(x => x.Date >= dateStart && x.Date <= dateEnd)
                                        .ToListAsync(cancelToken);
            return sheets;
        }
    }
}
