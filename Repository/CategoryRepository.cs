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
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<PagedList<Category>> GetAllCategoriesAsync(PaginationParameters paginationParameters)
        {
            return await Task.Run(() =>
                PagedList<Category>.ToPagedList(FindAll().Include(x => x.Produits).Include(x => x.Services).OrderBy(x => x.Libelle),
                    paginationParameters.PageNumber,
                    paginationParameters.PageSize)
                );
        }

        public async Task<Category> GetCategoryByIdAsync(Guid id)
        {
            return await FindByCondition(category => category.Id.Equals(id))
                .Include(x => x.Produits)
                .OrderBy(x => x.Libelle)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> CategoryExistAsync(Category category)
        {
            return await FindByCondition(x => x.Libelle == category.Libelle)
                .AnyAsync();
        }

        public async Task CreateCategoryAsync(Category category)
        {
            await CreateAsync(category);
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            await UpdateAsync(category);
        }

        public async Task DeleteCategoryAsync(Category category)
        {
            await DeleteAsync(category);
        }
    }
}
