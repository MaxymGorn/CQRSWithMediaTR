using Customer.Data;
using Customer.Data.Repositories;
using Customer.Domain.Models;

namespace Shop.Data.Repositories
{
    public class PhotoRepository : Repository<Photo>
    {
        public PhotoRepository(CustomerDbContext dbContext) : base(dbContext)
        {

        }
    }
}
