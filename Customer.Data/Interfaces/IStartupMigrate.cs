using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Data.Interfaces
{
    public interface IStartupMigrate
    {
        public Task MigrateAsync();
    }
}
