using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Data
{
    public interface IDbContext
    {
        public Task MigrateAsync();
    }
}
