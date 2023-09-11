using DATA.Repository.Implementation;
using Microsoft.EntityFrameworkCore;
using Spike.Models;

namespace Spike
{
    public class SpikeContext : DataDBContext
    {
        public SpikeContext(DbContextOptions<DataDBContext> options) : base(options)
        {
        }

        public SpikeContext() { }   
       

        public DbSet<DocumentV1> DocumentsV1 { get; set; }
        public DbSet<AuthorV1> AuthorsV1 { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Author < -- > Document
            modelBuilder.Entity<AuthorV1>().HasMany(e => e.DocumentsV1).WithMany(e => e.AuthorsV1);
        }
    }
}
