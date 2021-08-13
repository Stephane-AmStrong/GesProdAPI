using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IClientRepository
    {
        Task<PagedList<Client>> GetAllClientsAsync(PaginationParameters paginationParameters);

        Task<Client> GetClientByIdAsync(Guid id);
        Task<bool> ClientExistAsync(Client client);

        Task CreateClientAsync(Client client);
        Task UpdateClientAsync(Client client);
        Task DeleteClientAsync(Client client);
    }
}
