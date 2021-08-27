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
    public class SiteRepository : RepositoryBase<Site>, ISiteRepository
    {
        public SiteRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<PagedList<Site>> GetAllSitesAsync(PaginationParameters paginationParameters)
        {
            return await Task.Run(() =>
                PagedList<Site>.ToPagedList(FindAll().OrderBy(x => x.Libelle),
                    paginationParameters.PageNumber,
                    paginationParameters.PageSize)
                );
        }

        public async Task<Site> GetSiteByIdAsync(Guid id)
        {
            return await FindByCondition(site => site.Id.Equals(id))
                
                .OrderBy(x => x.Libelle)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> SiteExistAsync(Site site)
        {
            return await FindByCondition(x => x.Libelle == site.Libelle)
                .AnyAsync();
        }

        public async Task CreateSiteAsync(Site site)
        {
            await CreateAsync(site);
        }

        public async Task UpdateSiteAsync(Site site)
        {
            await UpdateAsync(site);
        }

        public async Task DeleteSiteAsync(Site site)
        {
            await DeleteAsync(site);
        }
    }
}
