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
    public class UtilisateurRepository : RepositoryBase<Utilisateur>, IUtilisateurRepository
    {
        public UtilisateurRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<PagedList<Utilisateur>> GetAllUtilisateursAsync(PaginationParameters paginationParameters)
        {
            return await Task.Run(() =>
                PagedList<Utilisateur>.ToPagedList(FindAll().OrderBy(x => x.Nom).ThenBy(x => x.Prenom),
                    paginationParameters.PageNumber,
                    paginationParameters.PageSize)
                );
        }

        public async Task<Utilisateur> GetUtilisateurByIdAsync(Guid id)
        {
            return await FindByCondition(utilisateur => utilisateur.Id.Equals(id))
                .OrderBy(x => x.Nom)
                .OrderBy(x => x.Prenom)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> UtilisateurExistAsync(Utilisateur utilisateur)
        {
            return await FindByCondition(x => x.Nom == utilisateur.Nom && x.Prenom == utilisateur.Prenom)
                .AnyAsync();
        }

        public async Task CreateUtilisateurAsync(Utilisateur utilisateur)
        {
            await CreateAsync(utilisateur);
        }

        public async Task UpdateUtilisateurAsync(Utilisateur utilisateur)
        {
            await UpdateAsync(utilisateur);
        }

        public async Task DeleteUtilisateurAsync(Utilisateur utilisateur)
        {
            await DeleteAsync(utilisateur);
        }
    }
}
