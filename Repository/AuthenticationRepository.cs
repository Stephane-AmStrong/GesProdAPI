using Contracts;
using Entities;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebMatrix.WebData;

namespace Repository
{
    public class AuthenticationRepository : RepositoryBase<AppUser>, IAuthenticationRepository
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;

        public AuthenticationRepository(RepositoryContext repositoryContext, UserManager<AppUser> userManager, IConfiguration configuration) : base(repositoryContext)
        {
            _configuration = configuration;
            _userManager = userManager;
        }


        public async Task<PagedList<AppUser>> GetAllUsersAsync(PaginationParameters paginationParameters)
        {
            return await Task.Run(() =>
                        PagedList<AppUser>.ToPagedList(FindByCondition(x=>x.IsCustomer == false).OrderBy(x => x.Name),
                            paginationParameters.PageNumber,
                            paginationParameters.PageSize)
                        );
        }


        public async Task<PagedList<AppUser>> GetAllCustomersAsync(PaginationParameters paginationParameters)
        {
            return await Task.Run(() =>
                        PagedList<AppUser>.ToPagedList(FindByCondition(x=>x.IsCustomer == true).OrderBy(x => x.Name),
                            paginationParameters.PageNumber,
                            paginationParameters.PageSize)
                        );
        }


        public async Task<AuthenticationResponse> LoginWithUserNameAsync(LoginRequest loginRequest, string password)
        {
            if (loginRequest == null) throw new ArgumentNullException(nameof(loginRequest));

            var user = await _userManager.FindByEmailAsync(loginRequest.Email);

            if (user == null)
            {
                return new AuthenticationResponse
                {
                    Message = "There is no user with that email adresse",
                    IsSuccess = false,
                };
            }

            var resultSucceeded = await _userManager.CheckPasswordAsync(user, password);

            if (!resultSucceeded)
            {
                return new AuthenticationResponse
                {
                    Message = "Invalid password !",
                    IsSuccess = false,
                };
            }

            var claims = new[]
            {
                new Claim("Email", user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim("Name", user.Name),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["AuthSettings:Issuer"],
                audience: _configuration["AuthSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            var userInfo = new Dictionary<string, string>
            {
                { "ImgUrl", user.ImgUrl },
                { "Name", user.Name},
                { "Email", user.Email },
                { "PhoneNumber", user.PhoneNumber },
                { "CreatedAt", user.CreatedAt.ToString()},
                { "UpdatedAt", user.UpdatedAt.ToString()},
                { "DisabledAt", user.DisabledAt.ToString()},
            };

            return new AuthenticationResponse
            {
                UserInfo = userInfo,
                AppUser = user,
                Token = tokenString,
                IsSuccess = true,
                ExpireDate = token.ValidTo,
            };
        }


        public async Task<AuthenticationResponse> RegisterUserAsync(AppUser user, string password)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            user.CreatedAt = DateTime.Now;
            user.UpdatedAt = DateTime.Now;

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                return new AuthenticationResponse
                {
                    IsSuccess = true,
                };
            }

            return new AuthenticationResponse
            {
                Message = "AppUser is not created",
                IsSuccess = false,
                ErrorDetails = result.Errors.Select(errorDescription => errorDescription.Description)
            };
        }




        public async Task<int> CountAllUsersAsync()
        {
            return await FindAll().CountAsync();
        }
    }
}
