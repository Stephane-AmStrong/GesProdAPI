using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IVenteRepository
    {
        Task<PagedList<Vente>> GetAllVentesAsync(VenteParameters venteParameters);

        Task<Vente> GetVenteByIdAsync(Guid id);

        Task CreateVenteAsync(Vente vente);
        Task UpdateVenteAsync(Vente vente);
        Task DeleteVenteAsync(Vente vente);
        Task<long> GetNextNumberAsync();

    }
}
