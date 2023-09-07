using DATA.Repository.Abstraction;
using DATA.Repository.Implementation.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using static DATA.Repository.Configuration.Config;

namespace DATA.Repository.Implementation
{
    public class DataDBContext : DbContext, IDataDBContext
    {
        static List<Type> _historicEntityTypesCache = new();
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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var historicEntities = GetHistoricEntityTypes();

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
            var entries = ChangeTracker.Entries().Where(e => e.Entity is BaseEntity && e.State == EntityState.Deleted);
            foreach (var entry in entries)
            {
                entry.State = EntityState.Modified;
                ((BaseEntity)entry.Entity).IsDeleted = true;
            }
        }
    }
}
