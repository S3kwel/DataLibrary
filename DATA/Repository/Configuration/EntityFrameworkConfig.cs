﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DATA.Repository.Implementation;

namespace DATA.Repository.Configuration
{
    public class Config {

        public class HistoricEntityConfiguration<T> : IEntityTypeConfiguration<T>
          where T : HistoricEntity
        {
            public void Configure(EntityTypeBuilder<T> builder)
            {
                builder.ToView(nameof(T) + "History");
                builder.Property(e=> e.PeriodStart).IsRequired();
                builder.Property(e=> e.PeriodEnd).IsRequired();
                builder.Property(e=> e.VersionTag).IsRequired().HasDefaultValueSql("NEWID()");
            }
        }
    }


}
