using System.Threading;
using System.Threading.Tasks;
using Timesheets.DataBase.Repositories.Interfaces;
using Timesheets.Entities;
using Timesheets.Services.Interfaces;

namespace Timesheets.Services
{
    public sealed class ContractsService : IContractsService
    {
        private readonly IContractsRepository _contractsRepository;

        public ContractsService(IContractsRepository repository)
        {
            _contractsRepository = repository;
        }

        public async Task<bool?> CheckContractIsActiveAsync(int id, CancellationToken cancelToken)
        {
            return await _contractsRepository.CheckContractIsActive(id, cancelToken);
        }
        public async Task<Contract> GetAsync(int id, CancellationToken cancelToken)
        {
            return await _contractsRepository.GetAsync(id, cancelToken);
        }
    }
}
