using DATA.Repository.Abstraction;
using DATA.Repository.Abstraction.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text.RegularExpressions;

namespace DATA.Repository.Implementation
{
    public class DataDBContext<TContext> : DbContext, IDataDBContext
        where TContext : DbContext
    {
        static List<Type> _historicEntityTypesCache = new();
        static List<Type> _entityTypeCache = new();
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
            if (_entityTypeCache.Count != 0)
                return _entityTypeCache;

            var baseEntities = new List<Type>();

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    var types = assembly.GetTypes()
                        .Where(p => p.IsClass && typeof(IBaseEntity).IsAssignableFrom(p));

                    baseEntities.AddRange(types);
                }
                catch (ReflectionTypeLoadException)
                {
                    //TODO: LOG
                }
            }

            _entityTypeCache = baseEntities;
            return baseEntities;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var historicEntities = GetHistoricEntityTypes();
            var baseEntities = GetBaseEntityTypes();

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                // Check if the entity is a base entity and if a historic counterpart exists.
                if (!(typeof(IHistoricEntity).IsAssignableFrom(entityType.ClrType)) &&
                    (typeof(IBaseEntity).IsAssignableFrom(entityType.ClrType)) &&
                    TryGetHistoricEntity(entityType, modelBuilder, out var historicEntityType))
                {

                    string baseEntityName = entityType.ClrType.Name;
                    string historicEntityName = historicEntityType.ClrType.Name;
                    Console.WriteLine(baseEntityName + " <> " + historicEntityName);

                    // Configure the base entity to use the historic entity's table name as the history table name.
                    modelBuilder.Entity(entityType.ClrType)
                        .ToTable(baseEntityName, e => e.IsTemporal(o => o.UseHistoryTable(historicEntityName)));

                   
                }
            }


            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                // Check if the entity type is a join entity implementing IJoinEntity<T1, T2>
                if (TryGetJoinEntity(entityType, out var entityType1, out var entityType2))
                {
                    // Construct the name for the join entity using the CLR type names
                    string joinEntityName = $"{entityType1.Name}{entityType2.Name}History";
                    Console.WriteLine("***" + joinEntityName + "***");
                    // Configure the join entity as needed
                    modelBuilder.Entity(entityType.ClrType)
                        .ToView(joinEntityName); // You can customize table or view names here
                }
            }

            // REFACTOR:  Only do this if there were any matches.  
            // Configure historic entities as views.
            foreach (var entityType in modelBuilder.Model.GetEntityTypes()
                .Where(et => typeof(IHistoricEntity).IsAssignableFrom(et.ClrType)))
            {

                modelBuilder.Entity(entityType.ClrType).ToView(entityType.GetTableName());

            }





        }

        // Utility method to access DbSet dynamically
        public DbSet<TEntity> DbSet<TEntity>() where TEntity : class
        {
            // Use reflection to find the DbSet property based on the TEntity type
            PropertyInfo dbSetProperty = GetType().GetProperties()
                .SingleOrDefault(p => p.PropertyType == typeof(DbSet<TEntity>));

            if (dbSetProperty == null)
            {
                throw new InvalidOperationException($"DbSet<{typeof(TEntity).Name}> not found.");
            }

            return (DbSet<TEntity>)dbSetProperty.GetValue(this);
        }
    

    private bool TryGetHistoricEntity(
      IMutableEntityType baseEntityType,
      ModelBuilder modelBuilder,
      out IMutableEntityType historicEntityType)
        {

            // Get the name of the base entity without the "HistoricV" part.
            var baseEntityName = baseEntityType.ClrType.Name;


            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                Console.WriteLine(entityType.ClrType.Name);
                var entityNameMatch = Regex.Match(entityType.ClrType.Name, @"(.+?)HistoricV(\d+)");

                var message = entityNameMatch.Success ? "Item matched." : "Item did not match.";
                Console.WriteLine(message);

                if (entityNameMatch.Success)
                {
                    var entityName = Regex.Match(baseEntityType.ClrType.Name, @"(.+?)V\d+").Groups[1].Value;
                    var version = Regex.Match(baseEntityType.ClrType.Name, @"(.+?)V(\d+)").Groups[2].Value;

                    var tag = $"{entityName}HistoricV{version}";

                    if (entityType.ClrType.Name == tag)
                    {
                        historicEntityType = entityType;
                        return true;
                    }
                }
            }

            // If no historic entity is found, set historicEntityType to null.
            historicEntityType = null;
            return false;
        }

        private bool TryGetJoinEntity(IMutableEntityType entityType, out Type type1, out Type type2)
        {
            // Check if the entity type implements the IJoinEntity<T1, T2> interface
            if (entityType.ClrType.GetInterfaces()
                .Any(i => i.IsGenericType &&
                           i.GetGenericTypeDefinition() == typeof(IJoinEntity<,>)))
            {
                // Get the generic type arguments (T1 and T2) from the IJoinEntity<T1, T2> interface
                var genericArguments = entityType.ClrType.GetInterfaces()
                    .First(i => i.IsGenericType &&
                                i.GetGenericTypeDefinition() == typeof(IJoinEntity<,>))
                    .GetGenericArguments();

                // Ensure there are exactly two type arguments
                if (genericArguments.Length == 2)
                {
                    type1 = genericArguments[0];
                    type2 = genericArguments[1];
                    return true;
                }
            }

            // If it's not a join entity, set type1 and type2 to null and return false
            type1 = null;
            type2 = null;
            return false;
        }
    

        public DataDBContext(DbContextOptions<TContext> options)
        : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        public DataDBContext() { }

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
            var entries = ChangeTracker.Entries().Where(e => e.Entity is IBaseEntity && e.State == EntityState.Deleted);
            foreach (var entry in entries)
            {
                entry.State = EntityState.Modified;
                ((IBaseEntity)entry.Entity).IsDeleted = true;
            }
        }
    }
}
