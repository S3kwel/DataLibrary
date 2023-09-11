﻿using DATA.Repository.Implementation.Strategies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using DATA.Repository.Abstraction.Strategies;
using DATA.Repository.Abstraction.Models;
using DATA.Repository.Implementation;
using DATA.Repository.Abstraction;

namespace DATA.Repository.Configuration
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers the generic repositories used by DATA. This method sets up the necessary services for any entity type 
        /// that will be used with IRepository or IHistoricRepository.
        /// </summary>
        /// <typeparam name="TContext">The type of the user-defined DbContext that will be registered and used by the repositories.</typeparam>
        /// <param name="services">The IServiceCollection to add the services to.</param>
        /// <param name="options">Configuration options for the DbContext.</param>
        public static void AddData<TContext>(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
        where TContext : DataDBContext
        {
            //DBContext and related.  
            services.AddDbContext<TContext>(options);
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IHistoricRepository<>), typeof(HistoricRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();


            var entityTypes = typeof(TContext).Assembly.GetTypes().Where(t => !t.IsAbstract && typeof(HistoricEntity).IsAssignableFrom(t)).ToList();

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
