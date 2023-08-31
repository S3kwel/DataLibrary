using DATA.Repository.Abstraction;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DATA.Repository.Configuration
{
    public class Config
    {
        public interface IPeriodEntity
        {
            DateTime PeriodStart { get; set; }
            DateTime PeriodEnd { get; set; }
        }



        public class HistoricEntityConfiguration<T> : IEntityTypeConfiguration<T>
            where T : class, IBaseEntity<Guid>
        {
            public void Configure(EntityTypeBuilder<T> builder)
            {
                if (GlobalEFConfiguration.IsMigration)
                {
                    builder.ToTable(typeof(T).Name + "History", c =>
                    {
                        c.IsTemporal(e =>
                        {
                            e.HasPeriodEnd("ShadowPeriodEnd");
                            e.HasPeriodStart("ShadowPeriodStart");
                        });
                    });


                }
                else
                {
                    builder.ToView(typeof(T).Name + "History");
  
                    builder.Property(e => e.VersionTag).IsRequired().HasDefaultValueSql("NEWID()");
                }

            }
        }

        public class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T>
            where T : class, IBaseEntity<Guid>
        {
            public void Configure(EntityTypeBuilder<T> builder)
            {
                builder.Property(e => e.IsDeleted).IsRequired();
              
                builder.Property(e => e.VersionTag).IsRequired();
                builder.HasKey(e => e.PrimaryKey);
            }
        }
    }
}
