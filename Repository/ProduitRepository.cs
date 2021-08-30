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
    public class ProduitRepository : RepositoryBase<Produit>, IProduitRepository
    {
        public ProduitRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<PagedList<Produit>> GetAllProduitsAsync(PaginationParameters paginationParameters)
        {
            return await Task.Run(() =>
                PagedList<Produit>.ToPagedList(FindAll()
                //.Include(x => x.Category)
                .OrderBy(x => x.Libelle),
                    paginationParameters.PageNumber,
                    paginationParameters.PageSize)
                );
        }

        public async Task<Produit> GetProduitByIdAsync(Guid id)
        {
            return await FindByCondition(produit => produit.Id.Equals(id))
                //.Include(x => x.Category)
                .OrderBy(x => x.Libelle)
                .FirstOrDefaultAsync();
        }

        public async Task CreateProduitAsync(Produit produit)
        {
            await CreateAsync(produit);
        }

        public async Task UpdateProduitAsync(Produit produit)
        {
            await UpdateAsync(produit);
        }

        public async Task DeleteProduitAsync(Produit produit)
        {
            await DeleteAsync(produit);
        }

        public async Task<bool> ProduitExistAsync(Produit produit)
        {
            return await FindByCondition(x => x.Libelle == produit.Libelle)
                .AnyAsync();
        }
    }
}
