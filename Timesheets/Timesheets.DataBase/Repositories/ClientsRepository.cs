using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Timesheets.DataBase.Repositories.Interfaces;
using Timesheets.Entities;

namespace Timesheets.DataBase.Repositories
{
    public sealed class ClientsRepository : IClientsRepository
    {
        private readonly AppDbContext _context;

        public ClientsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Client> GetAsync(int id, CancellationToken cancelToken)
        {
            return await  _context.Clients.FirstOrDefaultAsync(i => i.Id == id, cancelToken);
        }
        public async Task<IReadOnlyCollection<Client>> GetRangeAsync(int skip, int take, CancellationToken cancelToken)
        {
            try
            {
                return await _context.Clients
                                        .Skip(skip)
                                        .Take(take)
                                        .ToListAsync(cancelToken);
            }
            catch
            {
                return null;
            }
        }
        public async Task<Client> CreateAsync(Client item, CancellationToken cancelToken)
        {
            var entityEntry = await _context.Clients.AddAsync(item, cancelToken);
            await _context.SaveChangesAsync(cancelToken);
            return entityEntry.Entity;
        }
        public async Task UpdateAsync(Client item, CancellationToken cancelToken)
        {
            var updateItem = await GetAsync(item.Id, cancelToken);
            if (updateItem is null)
            {
                return;
            }
            updateItem = new Client
            {
                IsDeleted = item.IsDeleted,
                UserId = item.UserId,
                User = item.User,
            };
            await _context.SaveChangesAsync(cancelToken);
        }
        public async Task DeleteAsync(int id, CancellationToken cancelToken)
        {
            var item = await _context.Clients.FirstOrDefaultAsync(i => i.Id == id, cancelToken);
            if(item is null)
            {
                return;
            }
            item.IsDeleted = true;
            await _context.SaveChangesAsync(cancelToken);
        }
    }
}
