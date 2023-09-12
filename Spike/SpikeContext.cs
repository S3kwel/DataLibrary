using DATA.Repository.Implementation;
using Microsoft.EntityFrameworkCore;
using Spike.Models;

namespace Spike
{
    public class SpikeContext : DataDBContext<SpikeContext>
    {
        public SpikeContext(DbContextOptions<SpikeContext> options) : base(options)
        {
        }

        public SpikeContext() { }   
       

        public DbSet<DocumentV1> DocumentsV1 { get; set; }
        public DbSet<AuthorV1> AuthorsV1 { get; set; }
        public DbSet<DocumentHistoricV1> DocumentsHistoricV1 { get; set; }
        public DbSet<AuthorHistoricV1> AuthorsHistoricV1 { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Author < -- > Document
            modelBuilder.Entity<AuthorV1>().HasMany(e => e.DocumentsV1).WithMany(e => e.AuthorsV1);
            
            //Historic Author < -- > Historic Document
            modelBuilder.Entity<AuthorHistoricV1>().HasMany(e => e.DocumentsHistoricV1).WithMany(e => e.AuthorsHistoricV1);
        }
    }
}
