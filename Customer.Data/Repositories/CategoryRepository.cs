using Customer.Data;
using Customer.Data.IRepositories;
using Customer.Data.Repositories;
using Customer.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Data.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(CustomerDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<bool> CategoryExistAsync(string categoryName)
        {
            return await FindAsync(e => e.CategoryName==categoryName); 
        }

        public async Task<Category> GetCategoryAsync(int id)
        {
            return await GetAsync(element=> element.CategoryId==id);
        }
    }
}
