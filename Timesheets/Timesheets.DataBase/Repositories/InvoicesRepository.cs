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
    public sealed class InvoicesRepository : IInvoicesRepository
    {
        private readonly AppDbContext _context;

        public InvoicesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Invoice> GetAsync(int id, CancellationToken cancelToken)
        {
            return await _context.Invoices.FirstOrDefaultAsync(i => i.Id == id, cancelToken);
        }
        public async Task<IReadOnlyCollection<Invoice>> GetRangeAsync(int skip, int take, CancellationToken cancelToken)
        {
            try
            {
                return await _context.Invoices.Skip(skip).Take(take).ToListAsync(cancelToken);
            }
            catch
            {
                return null;
            }
        }
        public async Task AddAsync(Invoice item, CancellationToken cancelToken)
        {
            await _context.Invoices.AddAsync(item, cancelToken);
            await _context.SaveChangesAsync(cancelToken);
        }
        public async Task UpdateAsync(Invoice item, CancellationToken cancelToken)
        {
            var updateItem = await GetAsync(item.Id, cancelToken);
            if (updateItem is null)
            {
                return;
            }
            updateItem = new Invoice
            {
                ContractId = item.ContractId,
                DateStart = item.DateStart,
                DateEnd = item.DateEnd,
                Sum = item.Sum,
                Contract = item.Contract,
            };
            await _context.SaveChangesAsync(cancelToken);
        }
    }
}
