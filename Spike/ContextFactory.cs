using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Spike
{
    public class DesignTimeTestContextFactory : IDesignTimeDbContextFactory<SpikeContext>
    {
        public SpikeContext CreateDbContext(string[] args)
        {

            IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) // Set the path to your configuration file
            .AddJsonFile("appsettings.json") // Load the appsettings.json file
            .Build();

            var optionsBuilder = new DbContextOptionsBuilder<SpikeContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("default"));

            return new SpikeContext(optionsBuilder.Options);
        }
    }




}
