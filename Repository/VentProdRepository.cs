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
    public class VentProdRepository : RepositoryBase<VentProd>, IVentProdRepository
    {
        public VentProdRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<PagedList<VentProd>> GetAllVentProdsAsync(PaginationParameters paginationParameters)
        {
            return await Task.Run(() =>
                PagedList<VentProd>.ToPagedList(FindAll()
                .Include(x => x.Service)
                .ThenInclude(x => x.Category)
                .Include(x => x.Vente)
                .ThenInclude(x => x.Client)
                .OrderByDescending(x => x.Vente.DateVent),
                    paginationParameters.PageNumber,
                    paginationParameters.PageSize)
                );
        }

        public async Task<VentProd> GetVentProdByIdAsync(Guid id)
        {
            return await FindByCondition(ventProd => ventProd.Id.Equals(id))
                .Include(x => x.Service)
                .ThenInclude(x => x.Category)
                .Include(x => x.Vente)
                .ThenInclude(x => x.Client)
                .OrderByDescending(x => x.Vente.DateVent)
                .FirstOrDefaultAsync();
        }

        public async Task CreateVentProdAsync(VentProd ventProd)
        {
            await CreateAsync(ventProd);
        }

        public async Task UpdateVentProdAsync(VentProd ventProd)
        {
            await UpdateAsync(ventProd);
        }

        public async Task DeleteVentProdAsync(VentProd ventProd)
        {
            await DeleteAsync(ventProd);
        }

        public async Task<long> GetTurnoverAsync(DateTime startingAt, DateTime endingAt)
        {
            return await FindByCondition(x => x.Vente.DateEcheance >= startingAt && x.Vente.DateEcheance < endingAt).SumAsync(x => x.PrixVente * x.QteVendu - x.MntRemise);
        }

        public async Task<long> GetTurnoverAsync(DateTime startingAt, DateTime endingAt, Category category)
        {
            return await FindByCondition(x => x.Vente.DateEcheance >= startingAt && x.Vente.DateEcheance < endingAt && x.Service.CategoriesId == category.Id).SumAsync(x => x.PrixVente * x.QteVendu - x.MntRemise);
        }

        public async Task<long> GetTurnoverAsync(DateTime startingAt, DateTime endingAt, Service service)
        {
            return await FindByCondition(x => x.Vente.DateEcheance >= startingAt && x.Vente.DateEcheance < endingAt && x.ServicesId == service.Id).SumAsync(x => x.PrixVente * x.QteVendu - x.MntRemise);
        }

        public async Task<long> GetTurnoverAsync(DateTime startingAt, DateTime endingAt, Guid utilisateurId)
        {
            return await FindByCondition(x => x.Vente.DateEcheance >= startingAt && x.Vente.DateEcheance < endingAt && x.Vente.IdUserEnr == utilisateurId).SumAsync(x => x.PrixVente * x.QteVendu - x.MntRemise);
        }
    }
}
