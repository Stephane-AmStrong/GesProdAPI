using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IProduitRepository
    {
        Task<PagedList<Produit>> GetAllProduitsAsync(PaginationParameters paginationParameters);

        Task<Produit> GetProduitByIdAsync(Guid id);

        Task<bool> ProduitExistAsync(Produit produit);
        Task CreateProduitAsync(Produit produit);
        Task UpdateProduitAsync(Produit produit);
        Task DeleteProduitAsync(Produit produit);
    }
}
