using Customer.Data;
using Customer.Data.IRepositories;
using Customer.Data.Repositories;
using Customer.Domain.Models;
using System.Threading.Tasks;

namespace Shop.Data.Repositories
{
    public class ManufacturerRepository : Repository<Manufacturer>, IManufacturerRepository
    {
        public ManufacturerRepository(CustomerDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<Manufacturer> GetManufacturerAsync(int id)
        {
            return await GetAsync(element => element.ManufacturerId == id);
        }

        public async Task<bool> ManufacturerExistAsync(string manufacturerName)
        {
            return await FindAsync(e => e.ManufacturerName == manufacturerName);
        }
    }
}
