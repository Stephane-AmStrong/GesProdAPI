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
    public class VenteRepository : RepositoryBase<Vente>, IVenteRepository
    {
        public VenteRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<PagedList<Vente>> GetAllVentesAsync(VenteParameters venteParameters)
        {
            var ventes = Enumerable.Empty<Vente>().AsQueryable();

            if(venteParameters.IdUserEnr == null && venteParameters.ClientId == null)
            {
                ventes = await Task.Run(() => FindAll()
                    .Include(x => x.Client)
                    .Include(x => x.VentProds)
                    .ThenInclude(x => x.Service)
                    .ThenInclude(x => x.Category)
                    .Include(x => x.VentProds)
                    .ThenInclude(x => x.Disponibilite)
                    .ThenInclude(x => x.Produit)
                    .OrderByDescending(x => x.DateVent));
            } 
            else if (venteParameters.IdUserEnr != null && venteParameters.ClientId == null)
            {
                ventes = await Task.Run(() => FindByCondition(x=> x.IdUserEnr == venteParameters.IdUserEnr)
                    .Include(x => x.Client)
                    .Include(x => x.VentProds)
                    .ThenInclude(x => x.Service)
                    .ThenInclude(x => x.Category)
                    .Include(x => x.VentProds)
                    .ThenInclude(x => x.Disponibilite)
                    .ThenInclude(x => x.Produit)
                    .OrderByDescending(x => x.DateVent));
            }
            else if (venteParameters.IdUserEnr == null && venteParameters.ClientId != null)
            {
                ventes = await Task.Run(() => FindByCondition(x => x.ClientId == venteParameters.ClientId)
                    .Include(x => x.Client)
                    .Include(x => x.VentProds)
                    .ThenInclude(x => x.Service)
                    .ThenInclude(x => x.Category)
                    .Include(x => x.VentProds)
                    .ThenInclude(x => x.Disponibilite)
                    .ThenInclude(x => x.Produit)
                    .OrderByDescending(x => x.DateVent));
            }
            else if (venteParameters.IdUserEnr != null && venteParameters.ClientId != null)
            {
                ventes = await Task.Run(() => FindByCondition(x => x.IdUserEnr == venteParameters.IdUserEnr && x.ClientId == venteParameters.ClientId)
                    .Include(x => x.Client)
                    .Include(x => x.VentProds)
                    .ThenInclude(x => x.Service)
                    .ThenInclude(x => x.Category)
                    .Include(x => x.VentProds)
                    .ThenInclude(x => x.Disponibilite)
                    .ThenInclude(x => x.Produit)
                    .OrderByDescending(x => x.DateVent));
            }


            return await Task.Run(() =>
                PagedList<Vente>.ToPagedList(ventes,
                    venteParameters.PageNumber,
                    venteParameters.PageSize)
                );
        }

        public async Task<Vente> GetVenteByIdAsync(Guid id)
        {
            return await FindByCondition(vente => vente.Id.Equals(id))
                .Include(x => x.Client)
                .Include(x => x.VentProds)
                .ThenInclude(x => x.Service)
                .ThenInclude(x => x.Category)
                .Include(x => x.VentProds)
                .ThenInclude(x => x.Disponibilite)
                .ThenInclude(x => x.Produit)
                .OrderByDescending(x => x.DateVent)
                .FirstOrDefaultAsync();
        }

        public async Task CreateVenteAsync(Vente vente)
        {
            await CreateAsync(vente);
        }

        public async Task UpdateVenteAsync(Vente vente)
        {
            await UpdateAsync(vente);
        }

        public async Task DeleteVenteAsync(Vente vente)
        {
            await DeleteAsync(vente);
        }

        public async Task<long> GetNextNumberAsync()
        {
            return (!await FindAll().AnyAsync() ? 1 : ((await FindAll().MaxAsync(x => x.NumFact))) + 1);
        }
    }
}
