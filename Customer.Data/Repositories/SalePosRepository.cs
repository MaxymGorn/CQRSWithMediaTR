using Customer.Data;
using Customer.Data.Repositories;
using Customer.Domain.Models;

namespace Shop.Data.Repositories
{
    public class SalePosRepository : Repository<SalePos>
    {
        public SalePosRepository(CustomerDbContext dbContext) : base(dbContext)
        {

        }
    }
}
