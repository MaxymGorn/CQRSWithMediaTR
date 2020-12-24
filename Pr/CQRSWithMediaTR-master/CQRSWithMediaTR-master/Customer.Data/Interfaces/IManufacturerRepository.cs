using Customer.Data.Repositories;
using Customer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Data.IRepositories
{
    public interface IManufacturerRepository : IRepository<Manufacturer>
    {
        Task<bool> ManufacturerExistAsync(string manufacturerName);
        Task<Manufacturer> GetManufacturerAsync(int id);
    }
}
