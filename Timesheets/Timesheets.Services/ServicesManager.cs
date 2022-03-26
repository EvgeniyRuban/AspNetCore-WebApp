using System.Threading;
using System.Threading.Tasks;
using Timesheets.DataBase.Repositories.Interfaces;
using Timesheets.Entities;
using Timesheets.Services.Interfaces;

namespace Timesheets.Services
{
    public sealed class ServicesManager : IServicesManager
    {
        private readonly IServicesRepository _servicesRepository;

        public ServicesManager(IServicesRepository servicesRepository)
        {
            _servicesRepository = servicesRepository;
        }

        public async Task<Service> GetAsync(int id, CancellationToken cancelToken)
        {
            return await _servicesRepository.GetAsync(id, cancelToken);
        }
    }
}
