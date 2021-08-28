using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Contracts.IVentProdRepository;

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
        
        public async Task UpdateVentProdAsync(IEnumerable<VentProd> ventProds)
        {
            await UpdateAsync(ventProds);
        }
        
        public async Task DeleteVentProdAsync(VentProd ventProd)
        {
            await DeleteAsync(ventProd);
        }

        public async Task<long> GetTurnoverAsync(DateTime startingAt, DateTime endingAt, Target target)
        {
            if (target == Target.TheDeclaredOnes)
            {
                return (long)await FindByCondition(x => x.Vente.DateMecef!=null && x.Vente.DateEcheance >= startingAt && x.Vente.DateEcheance < endingAt).SumAsync(x => x.PrixVente * x.QteVendu - x.MntRemise);
            }

            return (long)await FindByCondition(x => x.Vente.DateMecef == null && x.Vente.DateEcheance >= startingAt && x.Vente.DateEcheance < endingAt).SumAsync(x => x.PrixVente * x.QteVendu - x.MntRemise);
        }

        public async Task<long> GetTurnoverAsync(DateTime startingAt, DateTime endingAt, Category category, Target target)
        {
            if (target == Target.TheDeclaredOnes)
            {
                return (long)await FindByCondition(x => x.Vente.DateMecef != null && x.Vente.DateEcheance >= startingAt && x.Vente.DateEcheance < endingAt && (x.Service.CategoriesId == category.Id || x.Disponibilite.Produit.CategoriesId == category.Id)).SumAsync(x => x.PrixVente * x.QteVendu - x.MntRemise);
            }

            return (long)await FindByCondition(x => x.Vente.DateMecef == null && x.Vente.DateEcheance >= startingAt && x.Vente.DateEcheance < endingAt && (x.Service.CategoriesId == category.Id || x.Disponibilite.Produit.CategoriesId == category.Id)).SumAsync(x => x.PrixVente * x.QteVendu - x.MntRemise);
        }

        public async Task<long> GetTurnoverAsync(DateTime startingAt, DateTime endingAt, Produit produit, Target target)
        {
            if (target == Target.TheDeclaredOnes)
            {
                return (long)await FindByCondition(x => x.Vente.DateMecef != null && x.Vente.DateEcheance >= startingAt && x.Vente.DateEcheance < endingAt && x.Disponibilite.ProduitsId == produit.Id).SumAsync(x => x.PrixVente * x.QteVendu - x.MntRemise);
            }

            return (long)await FindByCondition(x => x.Vente.DateMecef == null && x.Vente.DateEcheance >= startingAt && x.Vente.DateEcheance < endingAt && x.Disponibilite.ProduitsId == produit.Id).SumAsync(x => x.PrixVente * x.QteVendu - x.MntRemise);
        }
        
        public async Task<long> GetTurnoverAsync(DateTime startingAt, DateTime endingAt, Service service, Target target)
        {
            if (target == Target.TheDeclaredOnes)
            {
                return (long)await FindByCondition(x => x.Vente.DateMecef != null && x.Vente.DateEcheance >= startingAt && x.Vente.DateEcheance < endingAt && x.ServicesId == service.Id).SumAsync(x => x.PrixVente * x.QteVendu - x.MntRemise);
            }

            return (long)await FindByCondition(x => x.Vente.DateMecef == null && x.Vente.DateEcheance >= startingAt && x.Vente.DateEcheance < endingAt && x.ServicesId == service.Id).SumAsync(x => x.PrixVente * x.QteVendu - x.MntRemise);
        }

        public async Task<long> GetTurnoverAsync(DateTime startingAt, DateTime endingAt, Guid utilisateurId, Target target)
        {
            if (target == Target.TheDeclaredOnes)
            {
                return (long)await FindByCondition(x => x.Vente.DateMecef != null && x.Vente.DateEcheance >= startingAt && x.Vente.DateEcheance < endingAt && x.Vente.IdUserEnr == utilisateurId).SumAsync(x => x.PrixVente * x.QteVendu - x.MntRemise);
            }

            return (long)await FindByCondition(x => x.Vente.DateMecef == null && x.Vente.DateEcheance >= startingAt && x.Vente.DateEcheance < endingAt && x.Vente.IdUserEnr == utilisateurId).SumAsync(x => x.PrixVente * x.QteVendu - x.MntRemise);
        }
    }
}
