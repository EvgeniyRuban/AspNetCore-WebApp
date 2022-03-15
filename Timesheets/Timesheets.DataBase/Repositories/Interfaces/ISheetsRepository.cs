using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Timesheets.Entities;

namespace Timesheets.DataBase.Repositories.Interfaces
{
    public interface ISheetsRepository : IBaseRepository<Sheet>
    {
        Task<IEnumerable<Sheet>> GetItemsForInvoice(
            Guid contractId, 
            DateTime dateStart, 
            DateTime dateEnd, 
            CancellationToken cancelToken);
    }
}
