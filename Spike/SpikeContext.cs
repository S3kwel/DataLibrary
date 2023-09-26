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
        public DbSet<DocumentHistoricV1> DocumentHistoricV1 { get; set; }
        public DbSet<AuthorV1> AuthorsV1 { get; set; }
        public DbSet<AuthorHistoricV1> AuthorHistoricV1 { get; set; }
        public DbSet<DocumenV1AuthorV1HJoin> DocumentAuthorsV1H { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DocumentV1>().HasData(Seed.DocumentsV1); 
            modelBuilder.Entity<AuthorV1>().HasData(Seed.AuthorV1); 

            //Author < -- > Document
            //modelBuilder.Entity<AuthorV1>().HasMany(e => e.DocumentsV1).WithMany(e => e.AuthorsV1);
            
            //Historic Author < -- > Historic Document
            //modelBuilder.Entity<AuthorHistoricV1>().HasMany(e => e.DocumentsV1).WithMany(e => e.AuthorsV1);
        }
    }
}
