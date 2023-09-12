using DATA.Repository.Abstraction;
using DATA.Repository.Abstraction.Strategies;
using DATA.Repository.Implementation.Debugging;
using DATA.Repository.Implementation.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace DATA.Repository.Implementation
{
    public class UnitOfWork<TContext> : IDisposable, IUnitOfWork<TContext> where TContext : DbContext
    {
        private readonly TContext _dbContext;
        private readonly Dictionary<Type, object> _repositories;
        private readonly IServiceProvider _serviceProvider;

        public UnitOfWork(TContext dbContext, IServiceProvider provider)
        {
            _dbContext = dbContext;
            _repositories = new Dictionary<Type, object>();
            _serviceProvider = provider;

            if (_serviceProvider == null)
                throw new InvalidOperationException("Unit of Work must be used in a Dependency Injection Container!");

        }

        public IRepository<T> Repository<T>() where T : BaseEntity
        {
            if (_repositories.ContainsKey(typeof(T)))
            {
                return (IRepository<T>)_repositories[typeof(T)];
            }

            // Resolve dependencies from the service provider
            var debugStrategies = _serviceProvider.GetService<IDebugStrategy<T>>();
            if (debugStrategies == null)
            {
                throw new InvalidOperationException($"IDebugStrategy<{typeof(T).Name}> is not registered in the DI container.");
            }


            var repo = new Repository<T>(_dbContext, debugStrategies);
            _repositories.Add(typeof(T), repo);
            return repo;
        }

        public IHistoricRepository<T> HistoricRepository<T>() where T : HistoricEntity
        {
            if (_serviceProvider == null)
                throw new InvalidOperationException("Unit of Work must be used in a Dependency Injection Container!");

            if (_repositories.ContainsKey(typeof(T)))
            {
                return (IHistoricRepository<T>)_repositories[typeof(T)];
            }

            // Resolve dependencies from the service provider
            var queryStrategies = _serviceProvider.GetService<IEnumerable<IQueryStrategy<T>>>();
            if (queryStrategies == null)
            {
                throw new InvalidOperationException($"IQueryStrategy<{typeof(T).Name}> is not registered in the DI container.");
            }

            var debugStrategies = _serviceProvider.GetService<IDebugStrategy<T>>();
            if (debugStrategies == null)
            {
                throw new InvalidOperationException($"IDebugStrategy<{typeof(T).Name}> is not registered in the DI container.");
            }

            var repo = new HistoricRepository<T>(_dbContext, queryStrategies, debugStrategies);


            _repositories.Add(typeof(T), repo);
            return repo;
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _dbContext.Database.BeginTransaction();
        }

        public void Rollback()
        {
            _dbContext.Database.CurrentTransaction?.Rollback();
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}
