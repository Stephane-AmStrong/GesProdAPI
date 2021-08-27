using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ISiteRepository
    {
        Task<PagedList<Site>> GetAllSitesAsync(PaginationParameters paginationParameters);

        Task<Site> GetSiteByIdAsync(Guid id);
        Task<bool> SiteExistAsync(Site site);

        Task CreateSiteAsync(Site site);
        Task UpdateSiteAsync(Site site);
        Task DeleteSiteAsync(Site site);
    }
}
