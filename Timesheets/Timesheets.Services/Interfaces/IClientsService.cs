using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Timesheets.Entities.Dto;

namespace Timesheets.Services.Interfaces
{
    public interface IClientsService
    {
        Task<ClientResponse> GetAsync(int id, CancellationToken cancelToken);
        Task<IReadOnlyCollection<ClientResponse>> GetRange(int skip, int take, CancellationToken cancelToken);
        Task<ClientResponse> CreateAsync(ClientRequest request, CancellationToken cancelToken);
        Task<ClientResponse> UpdateAsync(ClientRequest request, CancellationToken cancelToken);
        Task DeleteAsync(int id, CancellationToken cancelToken);

    }
}
