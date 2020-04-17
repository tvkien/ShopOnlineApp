using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ShopOnline.Data.EntityFramework
{
    class ShopDBContextFactory : IDesignTimeDbContextFactory<ShopDBContext>
    {
        public ShopDBContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("ShopDatabase");
            var optionsBuilder = new DbContextOptionsBuilder<ShopDBContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new ShopDBContext(optionsBuilder.Options);
        }
    }
}