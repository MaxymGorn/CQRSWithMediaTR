using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Customer.API
{
    public static class Extensions
    {
        /// <summary>
        ///     Auto migrate version of database from migrations
        /// </summary>
        public static async System.Threading.Tasks.Task<IApplicationBuilder> UseAutoMigrateDatabaseAsync<TDbContext>(this IApplicationBuilder builder)
            where TDbContext : DbContext
        {
            using (IServiceScope serviceScope =
                builder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<IMigrator>();
                await serviceScope.ServiceProvider.GetService<TDbContext>().Database.MigrateAsync();
            }

            return builder;
        }
    }
}