using System;
using System.Threading.Tasks;
using Timesheets.Services.Interfaces;

namespace Timesheets.Services
{
    public sealed class ContractsService : IContractsService
    {
        public Task<bool?> CheckContractIsActive(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
