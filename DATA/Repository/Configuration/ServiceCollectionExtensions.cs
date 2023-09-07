using DATA.Repository.Implementation.Strategies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using DATA.Repository.Abstraction.Strategies;
using DATA.Repository.Abstraction.Models;

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

    public static class StrategyRegistrationFactory
    {
        public static void RegisterStrategiesForAllEntities(IServiceCollection services, Assembly targetAssembly)
        {
            var entityTypes = targetAssembly.GetTypes().Where(t => !t.IsAbstract && typeof(IBaseEntity).IsAssignableFrom(t)).ToList();

            foreach (var entityType in entityTypes)
            {
                var allTimeStrategyType = typeof(AllTimeStrategy<>).MakeGenericType(entityType);
                var activeWithinStrategyType = typeof(ActiveWithinStrategy<>).MakeGenericType(entityType);
                var atExactTimeType = typeof(AtExactTimeStrategy<>).MakeGenericType(entityType);
                var activeBetweenStrategy = typeof(ActiveBetweenStrategy<>).MakeGenericType(entityType);
                var activeThroughStrategy = typeof(ActiveThroughStrategy<>).MakeGenericType(entityType);

                services.AddTransient(typeof(IQueryStrategy<>).MakeGenericType(entityType), allTimeStrategyType);
                services.AddTransient(typeof(IQueryStrategy<>).MakeGenericType(entityType), activeWithinStrategyType);
                services.AddTransient(typeof(IQueryStrategy<>).MakeGenericType(entityType), atExactTimeType);
                services.AddTransient(typeof(IQueryStrategy<>).MakeGenericType(entityType), activeBetweenStrategy);
                services.AddTransient(typeof(IQueryStrategy<>).MakeGenericType(entityType), activeThroughStrategy);
            }
        }
    }


}
