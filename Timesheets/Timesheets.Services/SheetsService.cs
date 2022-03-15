using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheets.Entities;
using Timesheets.Services.Interfaces;

namespace Timesheets.Services
{
    public sealed class SheetsService : ISheetsService
    {
        public Task<Guid> Create(SheetRequest sheet)
        {
            throw new NotImplementedException();
        }

        public Task<Sheet> GetItem(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Sheet>> GetItems()
        {
            throw new NotImplementedException();
        }

        public Task Update(Guid id, SheetRequest sheetRequest)
        {
            throw new NotImplementedException();
        }
    }
}
