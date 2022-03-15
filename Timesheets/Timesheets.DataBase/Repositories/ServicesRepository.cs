using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Timesheets.DataBase.Repositories.Interfaces;
using Timesheets.Entities;

namespace Timesheets.DataBase.Repositories
{
    public sealed class ServicesRepository : IServicesRepository
    {
        private readonly AppDbContext _context;

        public ServicesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Service> GetAsync(int id, CancellationToken cancelToken)
        {
            return await _context.Services.FirstOrDefaultAsync(i => i.Id == id, cancelToken);
        }
        public async Task<IReadOnlyCollection<Service>> GetRangeAsync(int skip, int take, CancellationToken cancelToken)
        {
            try
            {
                return await  _context.Services
                                        .Skip(skip)
                                        .Take(take)
                                        .ToListAsync(cancelToken);
            }
            catch
            {
                return null;
            }
        }
        public async Task AddAsync(Service item, CancellationToken cancelToken)
        {
            await _context.Services.AddAsync(item, cancelToken);
            await _context.SaveChangesAsync(cancelToken);
        }
        public async Task UpdateAsync(Service item, CancellationToken cancelToken)
        {
            var updateItem = await GetAsync(item.Id, cancelToken);
            if (updateItem is null)
            {
                return;
            }
            updateItem = new Service
            {
                Name = item.Name,
                Sheets = item.Sheets,
            };
            await _context.SaveChangesAsync(cancelToken);
        }
    }
}
