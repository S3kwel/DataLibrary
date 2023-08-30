using DATA.Repository.Abstraction;
using DATA.Repository.Implementation.Strategies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using DATA.Repository.Abstraction.Strategies;
using DATA.Repository.Implementation;

namespace DATA.Repository.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static void AddData<TContext>(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
            where TContext : DbContext  
        {
            services.AddDbContext<TContext>(options);
            //services.AddScoped<IUnitOfWork, UnitOfWork>();

        }


    }

    public static class StrategyRegistrationFactory<T,TKey>
        where T : BaseEntity<TKey>
    {
        public static void RegisterStrategiesForAllEntities(IServiceCollection services, Assembly targetAssembly)
        {
            var entityTypes = targetAssembly.GetTypes().Where(t => !t.IsAbstract && typeof(IBaseEntity<TKey>).IsAssignableFrom(t)).ToList();

            foreach (var entityType in entityTypes)
            {
                var allTimeStrategyType = typeof(AllTimeStrategy<T,TKey>).MakeGenericType(entityType);
                var activeWithinStrategyType = typeof(ActiveWithinStrategy<T, TKey>).MakeGenericType(entityType);
                var atExactTimeType = typeof(AtExactTimeStrategy<T, TKey>).MakeGenericType(entityType);
                var activeBetweenStrategy = typeof(ActiveBetweenStrategy<T, TKey>).MakeGenericType(entityType);
                var activeThroughStrategy = typeof(ActiveThroughStrategy<T,TKey>).MakeGenericType(entityType);

                services.AddTransient(typeof(IQueryStrategy<T, TKey>).MakeGenericType(entityType), allTimeStrategyType);
                services.AddTransient(typeof(IQueryStrategy<T, TKey>).MakeGenericType(entityType), activeWithinStrategyType);
                services.AddTransient(typeof(IQueryStrategy<T, TKey>).MakeGenericType(entityType), atExactTimeType);
                services.AddTransient(typeof(IQueryStrategy<T, TKey>).MakeGenericType(entityType), activeBetweenStrategy);
                services.AddTransient(typeof(IQueryStrategy<T, TKey>).MakeGenericType(entityType), activeThroughStrategy);
            }
        }
    }


}
