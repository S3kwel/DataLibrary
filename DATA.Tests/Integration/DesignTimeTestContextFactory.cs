using DATA.Tests.Integration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DATA.Repository.Configuration
{


    public class DesignTimeTestContextFactory : IDesignTimeDbContextFactory<TestContext>
    {
        public TestContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TestContext>();
            optionsBuilder.UseSqlServer("Server=SEKWEL;Database=master;User Id=sa;Password=test");

            return new TestContext(optionsBuilder.Options);
        }
    }
}
