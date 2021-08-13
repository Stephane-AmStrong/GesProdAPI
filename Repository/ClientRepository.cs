using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ClientRepository : RepositoryBase<Client>, IClientRepository
    {
        public ClientRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<PagedList<Client>> GetAllClientsAsync(PaginationParameters paginationParameters)
        {
            return await Task.Run(() =>
                PagedList<Client>.ToPagedList(FindAll().Include(x => x.Ventes).OrderBy(x => x.NomEntreprise),
                    paginationParameters.PageNumber,
                    paginationParameters.PageSize)
                );
        }

        public async Task<Client> GetClientByIdAsync(Guid id)
        {
            return await FindByCondition(client => client.Id.Equals(id))
                
                .OrderBy(x => x.NomEntreprise)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> ClientExistAsync(Client client)
        {
            return await FindByCondition(x => x.NomEntreprise == client.NomEntreprise)
                .AnyAsync();
        }

        public async Task CreateClientAsync(Client client)
        {
            await CreateAsync(client);
        }

        public async Task UpdateClientAsync(Client client)
        {
            await UpdateAsync(client);
        }

        public async Task DeleteClientAsync(Client client)
        {
            await DeleteAsync(client);
        }
    }
}
