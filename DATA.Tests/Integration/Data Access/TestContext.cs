using DATA.Repository.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DATA.Tests.Integration
{



    public class TestContextFactory : IDesignTimeDbContextFactory<TestContext> // Guid is used here as a common example.
    {


    

        public TestContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TestContext>();
            optionsBuilder.UseSqlServer("Server=SEKWEL;Database=dataTests;User Id=sa;Password=test;TrustServerCertificate=True;");

            return new TestContext(optionsBuilder.Options);
        }
    }


    public class TestContext : BaseDBContext<TestContext, Guid>
    {

        public Microsoft.EntityFrameworkCore.DbSet<Document<Guid>> Documents { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<HistoricDocument<Guid>> HistoricDocuments { get; set; }

        public TestContext(DbContextOptions<TestContext> options) : base(options) { }

        public static DbContextOptions<TestContext> CreateOptions()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TestContext>();
            optionsBuilder.UseSqlServer("YourConnectionStringHere");
            return optionsBuilder.Options;
        }

        public TestContext() : base(CreateOptions()) { }

       
    }

   
    

    public class Document<Guid> : BaseEntity<Guid>
    {
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty; 

    }

    public class HistoricDocument<TKey> : HistoricBaseEntity<TKey>
    {
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
    }
}











