using DATA.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using static DATA.Repository.Configuration.Config;

namespace DATA.Repository.Implementation
{
    public class BaseDBContext<TContext, TKey> : DbContext where TContext : DbContext
    {
        // Constructor accepting DbContextOptions
        public BaseDBContext(DbContextOptions<TContext> options) : base(options)
        {
        }

        static List<Type> _historicEntityTypesCache = new();
        static List<Type> _baseEntityCache = new();

        static IEnumerable<Type> GetHistoricEntityTypes()
        {
            if (_historicEntityTypesCache.Count != 0)
                return _historicEntityTypesCache;

            var historicEntities = new List<Type>();

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    var types = assembly.GetTypes()
                        .Where(p => p.IsClass && typeof(IHistoricEntity).IsAssignableFrom(p));

                    historicEntities.AddRange(types);
                }
                catch (ReflectionTypeLoadException)
                {
                    //TODO: LOG
                }
            }

            _historicEntityTypesCache = historicEntities;
            return historicEntities;
        }
        static IEnumerable<Type> GetBaseEntityTypes()
        {
            if (_baseEntityCache.Count != 0)
                return _baseEntityCache;

            var baseEntities = new List<Type>();

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    var types = assembly.GetTypes()
                        .Where(p => p.IsClass && typeof(IBaseEntity<TKey>).IsAssignableFrom(p));

                    baseEntities.AddRange(types);
                }
                catch (ReflectionTypeLoadException)
                {
                    //TODO: LOG
                }
            }

            _baseEntityCache = baseEntities;
            return baseEntities;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var historicEntities = GetHistoricEntityTypes();
            var baseEntities = GetBaseEntityTypes();


            foreach (var type in baseEntities)
            {
                var configurationInstance = Activator.CreateInstance(typeof(BaseEntityConfiguration<>).MakeGenericType(type));

                if (configurationInstance == null)
                {
                    throw new InvalidOperationException($"Failed to create a configuration instance for entity type: {type.FullName}");
                }

                modelBuilder.ApplyConfiguration((dynamic)configurationInstance!);
            }

            foreach (var type in historicEntities)
            {
                var configurationInstance = Activator.CreateInstance(typeof(HistoricEntityConfiguration<>).MakeGenericType(type));

                if (configurationInstance == null)
                {
                    throw new InvalidOperationException($"Failed to create a configuration instance for entity type: {type.FullName}");
                }

                modelBuilder.ApplyConfiguration((dynamic)configurationInstance!);
            }


        }

        public override int SaveChanges()
        {
            ApplySoftDeletes();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            ApplySoftDeletes();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void ApplySoftDeletes()
        {
            var entries = ChangeTracker.Entries().Where(e => e.Entity is BaseEntity<TKey> && e.State == EntityState.Deleted);
            foreach (var entry in entries)
            {
                entry.State = EntityState.Modified;
                ((BaseEntity<TKey>)entry.Entity).IsDeleted = true;
            }
        }
    }
}
