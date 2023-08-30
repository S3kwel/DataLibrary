using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DATA.Repository.Abstraction;
using DATA.Repository.Implementation;

namespace DATA.Repository.Configuration
{
    public class Config {

        public class HistoricEntityConfiguration<T> : IEntityTypeConfiguration<T>
          where T : class, IBaseEntity<Guid>
        {
            public void Configure(EntityTypeBuilder<T> builder)
            {
                builder.ToView(nameof(T) + "History");
                builder.Property(e=> e.PeriodStart).IsRequired();
                builder.Property(e=> e.PeriodEnd).IsRequired();
                builder.Property(e=> e.VersionTag).IsRequired().HasDefaultValueSql("NEWID()");
            }
        }

        public class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T>
            where T : class, IBaseEntity<Guid>
        {
            public void Configure(EntityTypeBuilder<T> builder)
            {
                // Set properties as required
                builder.Property(e => e.IsDeleted).IsRequired();
                builder.Property(e => e.PeriodStart).IsRequired();
                builder.Property(e => e.PeriodEnd).IsRequired();
                builder.Property(e => e.VersionTag).IsRequired();

                builder.HasKey(e => e.PrimaryKey);
            }
        }

    }


}
