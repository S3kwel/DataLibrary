using DATA.Tests.Integration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DATA.Repository.Configuration
{


    public class DesignTimeTestContextFactory<TKey> : IDesignTimeDbContextFactory<TestContext>
    {
        public TestContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TestContext>();
            optionsBuilder.UseSqlServer("Server=SEKWEL;Database=dataTests;User Id=sa;Password=test;TrustServerCertificate=True;");

            return new TestContext(optionsBuilder.Options);
        }
    }
}
