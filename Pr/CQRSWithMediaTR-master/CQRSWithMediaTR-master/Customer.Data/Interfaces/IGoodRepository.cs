using Customer.Data.IRepositories;
using Customer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Shop.Data.Interfaces
{
    public interface IGoodRepository : IRepository<Good>
    {
        Task<bool> GoodExistAsync(string goodName);
    }
}
