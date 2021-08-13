using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IServiceRepository
    {
        Task<PagedList<Service>> GetAllServicesAsync(PaginationParameters paginationParameters);
        Task<ICollection<Service>> GetAvailableServicesAsync(DateTime startingAt, DateTime endingAt);

        Task<Service> GetServiceByIdAsync(Guid id);

        Task<bool> ServiceExistAsync(Service service);
        Task CreateServiceAsync(Service service);
        Task UpdateServiceAsync(Service service);
        Task DeleteServiceAsync(Service service);
    }
}
