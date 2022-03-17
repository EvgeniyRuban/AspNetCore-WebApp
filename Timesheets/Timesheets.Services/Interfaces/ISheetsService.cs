﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Timesheets.Entities.Dto;

namespace Timesheets.Services.Interfaces
{
    public interface ISheetsService
    {
        Task<SheetResponse> GetAsync(int id, CancellationToken cancelToken);
        Task<IReadOnlyCollection<SheetResponse>> GetRangeAsync(int skip, int take, CancellationToken cancelToken);
        Task AddAsync(SheetRequest sheet, CancellationToken cancelToken);
        Task UpdateAsync(CreateSheetRequest sheetToUpdate, CancellationToken cancelToken);
    }
}