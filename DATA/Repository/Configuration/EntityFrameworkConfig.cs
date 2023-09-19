using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DATA.Repository.Abstraction;

namespace DATA.Repository.Configuration
{
    public class Config {

        public class HistoricEntityConfiguration<T> : IEntityTypeConfiguration<T>
          where T : class, IHistoricEntity
        {
            public void Configure(EntityTypeBuilder<T> builder)
            {
                builder.ToView(typeof(T).Name);
                builder.Ignore(e => e.PeriodStart);
                builder.Ignore(e => e.PeriodEnd);
                builder.Property(e=> e.VersionTag).IsRequired().HasDefaultValueSql("NEWID()");
            }
        }
    }


}
