using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheets.Entities;

namespace Timesheets.Services.Interfaces
{
    public interface ISheetsService
    {
        Task<Sheet> GetItem(Guid id);
        Task<IEnumerable<Sheet>> GetItems();
        Task<Guid> Create(SheetRequest sheet);
        Task Update(Guid id, SheetRequest sheetRequest);
    }
}
