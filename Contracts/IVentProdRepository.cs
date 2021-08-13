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

        Task<long> GetTurnoverAsync(DateTime startingAt, DateTime endingAt);
        Task<long> GetTurnoverAsync(DateTime startingAt, DateTime endingAt, Category category);
        Task<long> GetTurnoverAsync(DateTime startingAt, DateTime endingAt, Service service);
        Task<long> GetTurnoverAsync(DateTime startingAt, DateTime endingAt, Guid utilisateurId);

        Task<VentProd> GetVentProdByIdAsync(Guid id);

        Task CreateVentProdAsync(VentProd ventProd);
        Task UpdateVentProdAsync(VentProd ventProd);
        Task DeleteVentProdAsync(VentProd ventProd);
    }
}
