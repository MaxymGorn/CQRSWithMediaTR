using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Customer.Data
{
    public class CustomerDbContext : DbContext
    {
        public DbSet<Domain.Models.Customer> Customers { get; set; }
        public DbSet<Domain.Models.Category> Categories { get; set; }
        public DbSet<Domain.Models.Good> Goods { get; set; }
        public DbSet<Domain.Models.Manufacturer> Manufacturers { get; set; }
        public DbSet<Domain.Models.Photo> Photos { get; set; }
        public DbSet<Domain.Models.Sale> Sales { get; set; }
        public DbSet<Domain.Models.SalePos> SalePos { get; set; }

        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyAllConfigurations<CustomerDbContext>();
        }
        public async Task MigrateAsync()
        {
           await Database.MigrateAsync();
        }

        public override int SaveChanges()
        {
            this.AddAuditInfo();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            this.AddAuditInfo();
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}