using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IVentProdRepository
    {
        Task<PagedList<VentProd>> GetAllVentProdsAsync(PaginationParameters paginationParameters);

        Task<long> GetTurnoverAsync(DateTime startingAt, DateTime endingAt, Target target);
        Task<long> GetTurnoverAsync(DateTime startingAt, DateTime endingAt, Category category, Target target);
        Task<long> GetTurnoverAsync(DateTime startingAt, DateTime endingAt, Produit produit, Target target);
        Task<long> GetTurnoverAsync(DateTime startingAt, DateTime endingAt, Service service, Target target);
        Task<long> GetTurnoverAsync(DateTime startingAt, DateTime endingAt, Guid utilisateurId, Target target);


        Task<VentProd> GetVentProdByIdAsync(Guid id);

        Task CreateVentProdAsync(VentProd ventProd);
        Task UpdateVentProdAsync(VentProd ventProd);
        Task UpdateVentProdAsync(IEnumerable<VentProd> ventProds);
        Task DeleteVentProdAsync(VentProd ventProd);

        public enum Target
        {
            TheDeclaredOnes,
            TheNonDeclaredOnes
        }
    }
}
