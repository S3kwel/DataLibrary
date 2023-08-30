using DATA.Repository.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DATA.Repository.Configuration;
using DATA.Repository.Abstraction;
using System;
using DATA.Repository.Implementation.PrimaryKey;

namespace DATA.Tests.Integration
{
    public class TestContext : BaseDBContext<TestContext, int>
    {
        public TestContext(DbContextOptions<TestContext> options) : base(options)
        {
            

        }

        private static DbContextOptions<TestContext> CreateOptions()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TestContext>();
            optionsBuilder.UseSqlServer("Server=SEKWEL;Database=master;User Id=sa;Password=test");
            return optionsBuilder.Options;
        }

        public TestContext() : base(CreateOptions())
        {
        }


    }

    public class Document: BaseEntity<int>, IPrimaryKey<int>
    {
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty; 

    }

    public class HistoricDocument : Document, IHistoricEntity
    {

    }
}











