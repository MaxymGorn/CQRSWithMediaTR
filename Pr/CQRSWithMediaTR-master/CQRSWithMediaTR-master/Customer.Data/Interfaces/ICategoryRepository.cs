using Customer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Data.IRepositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<bool> CategoryExistAsync(string categoryName);
        Task<Category> GetCategoryAsync(int id);
    }
}
