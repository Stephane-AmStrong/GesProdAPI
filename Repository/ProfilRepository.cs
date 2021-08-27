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
    public class ProfilRepository : RepositoryBase<Profil>, IProfilRepository
    {
        public ProfilRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<PagedList<Profil>> GetAllProfilsAsync(PaginationParameters paginationParameters)
        {
            return await Task.Run(() =>
                PagedList<Profil>.ToPagedList(FindAll()
                .OrderBy(x => x.Libelle),
                    paginationParameters.PageNumber,
                    paginationParameters.PageSize)
                );
        }

        public async Task<Profil> GetProfilByIdAsync(Guid id)
        {
            return await FindByCondition(profil => profil.Id.Equals(id))
                .OrderBy(x => x.Libelle)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> ProfilExistAsync(Profil profil)
        {
            return await FindByCondition(x => x.Libelle == profil.Libelle)
                .AnyAsync();
        }

        public async Task CreateProfilAsync(Profil profil)
        {
            await CreateAsync(profil);
        }

        public async Task UpdateProfilAsync(Profil profil)
        {
            await UpdateAsync(profil);
        }

        public async Task DeleteProfilAsync(Profil profil)
        {
            await DeleteAsync(profil);
        }
    }
}
