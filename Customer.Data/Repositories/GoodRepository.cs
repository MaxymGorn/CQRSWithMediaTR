using Customer.Data;
using Customer.Data.Repositories;
using Customer.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Shop.Data.Interfaces;
using System;
using System.Threading.Tasks;

namespace Shop.Data.Repositories
{
    public class GoodRepository : Repository<Good>, IGoodRepository
    {
        public GoodRepository(CustomerDbContext dbContext) : base(dbContext)
        {

        }
        public async Task<bool> GoodExistAsync(string goodName)
        {
            return await FindAsync(e => e.GoodName == goodName);
        }
    }
}
