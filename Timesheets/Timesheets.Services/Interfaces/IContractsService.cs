using System.Threading;
using System.Threading.Tasks;
using Timesheets.Entities.Dto;

namespace Timesheets.Services.Interfaces
{
    public interface IContractsService
    {
        Task<ContractResponse> CreateAsync(ContractRequest contract, CancellationToken cancelToken);
        Task<ContractResponse> GetAsync(int id, CancellationToken cancelToken);
        Task<bool?> CheckContractIsActiveAsync(int id, CancellationToken cancelToken);
        Task DeleteAsync(int id, CancellationToken cancelToken);
    }
}
