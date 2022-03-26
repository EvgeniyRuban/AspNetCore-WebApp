using System.Threading;
using System.Threading.Tasks;
using Timesheets.DataBase.Repositories.Interfaces;
using Timesheets.Entities;
using Timesheets.Entities.Dto;
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
        public async Task<ContractResponse> CreateAsync(ContractRequest request, CancellationToken cancelToken)
        {
            if(request is null)
            {
                return null;
            }
            var contract = new Contract
            {
                Title = request.Title,
                DateStart = request.DateStart,
                DateEnd = request.DateEnd,
            };
            var newContract = await _contractsRepository.CreateAsync(contract, cancelToken);
            if(newContract is null)
            {
                return null;
            }
            return new ContractResponse
            {
                Id = newContract.Id,
                Title = newContract.Title,
                DateStart = newContract.DateStart,
                DateEnd= newContract.DateEnd,
            };
        }
        public async Task<ContractResponse> GetAsync(int id, CancellationToken cancelToken)
        {
            var contract = await _contractsRepository.GetAsync(id, cancelToken);
            if(contract is null)
            {
                return null;
            }
            return new ContractResponse
            {
                Id = contract.Id,
                Title = contract.Title,
                DateStart = contract.DateStart,
                DateEnd = contract.DateEnd,
            };
        }
        public async Task DeleteAsync(int id, CancellationToken cancelToken)
        {
            await _contractsRepository.DeleteAsync(id,cancelToken);
        }
    }
}
