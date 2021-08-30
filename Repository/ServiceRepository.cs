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
    public class ServiceRepository : RepositoryBase<Service>, IServiceRepository
    {
        public ServiceRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<PagedList<Service>> GetAllServicesAsync(PaginationParameters paginationParameters)
        {
            return await Task.Run(() =>
                PagedList<Service>.ToPagedList(FindAll()
                //.Include(x => x.Category)
                .OrderBy(x => x.Libelle),
                    paginationParameters.PageNumber,
                    paginationParameters.PageSize)
                );
        }
        
        public async Task<ICollection<Service>> GetAllServicesAsync()
        {
            return await Task.Run(() =>FindAll().ToListAsync());
        }

        public async Task<Service> GetServiceByIdAsync(Guid id)
        {
            return await FindByCondition(service => service.Id.Equals(id))
                //.Include(x => x.Category)
                .OrderBy(x => x.Libelle)
                .FirstOrDefaultAsync();
        }

        public async Task CreateServiceAsync(Service service)
        {
            await CreateAsync(service);
        }

        public async Task UpdateServiceAsync(Service service)
        {
            await UpdateAsync(service);
        }

        public async Task DeleteServiceAsync(Service service)
        {
            await DeleteAsync(service);
        }

        public async Task<bool> ServiceExistAsync(Service service)
        {
            return await FindByCondition(x => x.Libelle == service.Libelle)
                .AnyAsync();
        }

        public async Task<ICollection<Service>> GetAvailableServicesAsync(DateTime startingAt, DateTime endingAt)
        {
            /*
            - une chambre est occupéé si la date de début désiré est comprise entre la datevente et la dateecheance ou la date de fin est comprise entre la datevente et la dateecheance
            - une chambre est disponible pour période si sa date de début souhaité
             */
            var availableServices = await FindAll()
                //.Include(x => x.Category)
                .OrderBy(x => x.Libelle).ToListAsync();

            var unAvailableServices = await FindByCondition(service => service.VentProds.Where(vp => ((vp.Vente.DateVent <= startingAt && vp.Vente.DateEcheance > startingAt) || (vp.Vente.DateVent <= endingAt && vp.Vente.DateEcheance > endingAt))).Any()).ToListAsync();

            foreach (var unAvailableService in unAvailableServices)
            {
                availableServices.RemoveAll(x => x.Id == unAvailableService.Id);
            }


            return availableServices;
        }
    }
}
