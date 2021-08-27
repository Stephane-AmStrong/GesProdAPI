using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IProfilRepository
    {
        Task<PagedList<Profil>> GetAllProfilsAsync(PaginationParameters paginationParameters);

        Task<Profil> GetProfilByIdAsync(Guid id);

        Task<bool> ProfilExistAsync(Profil profil);
        Task CreateProfilAsync(Profil profil);
        Task UpdateProfilAsync(Profil profil);
        Task DeleteProfilAsync(Profil profil);

    }
}
