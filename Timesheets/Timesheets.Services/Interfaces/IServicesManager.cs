using System.Threading;
using System.Threading.Tasks;
using Timesheets.Entities;

namespace Timesheets.Services.Interfaces
{
    public interface IServicesManager
    {
        Task<Service> GetAsync(int id, CancellationToken cancelToken);
    }
}
