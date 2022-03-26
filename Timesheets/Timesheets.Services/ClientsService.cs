using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Timesheets.DataBase.Repositories.Interfaces;
using Timesheets.Entities;
using Timesheets.Entities.Dto;
using Timesheets.Services.Interfaces;

namespace Timesheets.Services
{
    public sealed class ClientsService : IClientsService
    {
        private readonly IClientsRepository _clientsRepository;

        public ClientsService(IClientsRepository clientsRepository)
        {
            _clientsRepository = clientsRepository;
        }

        public async Task<ClientResponse> CreateAsync(ClientRequest request, CancellationToken cancelToken)
        {
            if(request is null)
            {
                return null;
            }
            var client = new Client
            {
                UserId = request.UserId,
            };
            var newClient = await _clientsRepository.CreateAsync(client, cancelToken);
            if(newClient is null)
            {
                return null;
            }
            return new ClientResponse
            {
                Id = newClient.Id,
                UserId = newClient.UserId,
            };
        }
        public async Task<ClientResponse> GetAsync(int id, CancellationToken cancelToken)
        {
            var client = await _clientsRepository.GetAsync(id, cancelToken);
            if(client is null)
            {
                return null;
            }
            return new ClientResponse
            {
                Id = client.Id,
                UserId= client.UserId,
            };
        }
        public Task<IReadOnlyCollection<ClientResponse>> GetRange(int skip, int take, CancellationToken cancelToken)
        {
            var clients = _clientsRepository.GetRangeAsync(skip, take, cancelToken);
            if(clients is null)
            {
                return null;
            }
            var clientsColletion = new List<ClientResponse>();
            foreach(var client in (IEnumerable)clients)
            {
                clientsColletion.Add(new ClientResponse
                {
                    Id = clien
                };
            }
        }
        public Task<ClientResponse> UpdateAsync(ClientRequest request, CancellationToken cancelToken)
        {
            throw new System.NotImplementedException();
        }
        public Task DeleteAsync(int id, CancellationToken cancelToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
