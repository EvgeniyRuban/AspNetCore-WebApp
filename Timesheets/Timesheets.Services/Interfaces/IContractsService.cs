using System;
using System.Threading.Tasks;

namespace Timesheets.Services.Interfaces
{
    public interface IContractsService
    {
        Task<bool?> CheckContractIsActive(Guid id);
    }
}
