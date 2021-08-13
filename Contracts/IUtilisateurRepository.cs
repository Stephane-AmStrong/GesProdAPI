using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IUtilisateurRepository
    {
        Task<PagedList<Utilisateur>> GetAllUtilisateursAsync(PaginationParameters paginationParameters);

        Task<Utilisateur> GetUtilisateurByIdAsync(Guid id);
        Task<bool> UtilisateurExistAsync(Utilisateur utilisateur);

        Task CreateUtilisateurAsync(Utilisateur utilisateur);
        Task UpdateUtilisateurAsync(Utilisateur utilisateur);
        Task DeleteUtilisateurAsync(Utilisateur utilisateur);
    }
}
