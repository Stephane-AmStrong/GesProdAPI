using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IAuthenticationRepository
    {
        Task<PagedList<AppUser>> GetAllUsersAsync(PaginationParameters paginationParameters);
        Task<PagedList<AppUser>> GetAllCustomersAsync(PaginationParameters paginationParameters);
        Task<int> CountAllUsersAsync();
        Task<AuthenticationResponse> RegisterUserAsync(AppUser AppUser, string password);
        Task<AuthenticationResponse> LoginWithUserNameAsync(LoginRequest loginRequest, string password);
    }
}
