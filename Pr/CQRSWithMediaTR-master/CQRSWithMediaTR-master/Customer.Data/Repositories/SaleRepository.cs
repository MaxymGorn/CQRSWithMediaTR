using Customer.Data;
using Customer.Data.Repositories;
using Customer.Domain.Models;

namespace Shop.Data.Repositories
{
    public class SaleRepository : Repository<Sale>
    {
        public SaleRepository(CustomerDbContext dbContext) : base(dbContext)
        {

        }
    }
}
