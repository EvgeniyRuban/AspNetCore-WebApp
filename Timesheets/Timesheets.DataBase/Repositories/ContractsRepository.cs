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
    public sealed class ContractsRepository : IContractsRepository
    {
        private readonly AppDbContext _context;

        public ContractsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Contract> GetAsync(Guid id, CancellationToken cancelToken)
        {
            return await _context.Contracts.FirstOrDefaultAsync(i => i.Id == id, cancelToken);
        }
        public async Task<IReadOnlyCollection<Contract>> GetRangeAsync(int skip, int take, CancellationToken cancelToken)
        {
            try
            {
                return await _context.Contracts.Skip(skip).Take(take).ToListAsync(cancelToken);
            }
            catch
            {
                return null;
            }
        }
        public async Task AddAsync(Contract item, CancellationToken cancelToken)
        {
            await _context.Contracts.AddAsync(item, cancelToken);
            await _context.SaveChangesAsync(cancelToken);
        }
        public async Task UpdateAsync(Contract item, CancellationToken cancelToken)
        {
            var updateItem = await GetAsync(item.Id, cancelToken);
            if (updateItem is null)
            {
                return;
            }
            updateItem = new Contract
            {
                Title = item.Title,
                DateStart = item.DateStart,
                DateEnd = item.DateEnd,
                IsDeleted = item.IsDeleted,
                Sheets = item.Sheets,
            };
            await _context.SaveChangesAsync(cancelToken);
        }
        public async Task DeleteAsync(Guid id, CancellationToken cancelToken)
        {
            var item = await  _context.Contracts
                                        .FirstOrDefaultAsync(i => i.Id == id, cancelToken);
            if (item is null)
            {
                return;
            }
            item.IsDeleted = true;
            await _context.SaveChangesAsync(cancelToken);
        }
        public async Task<bool?> CheckContractIsActive(Guid id, CancellationToken cancelToken)
        {
            var contract = await GetAsync(id, cancelToken);
            if(contract is null)
            {
                return null;
            }
            var timeNow = DateTime.UtcNow;
            return contract.DateStart <= timeNow && contract.DateEnd > timeNow;
        }
    }
}
