﻿using DATA.Repository.Implementation;
using Microsoft.EntityFrameworkCore;


namespace DATA.Repository.Abstraction.ConcurrencyHandling
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T, TKey> Repository<T, TKey>() where T : BaseEntity;
        void Save();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        private Dictionary<String, object>? _repositories;

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

       
        public IRepository<T, TKey> Repository<T, TKey>() where T : BaseEntity
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<string, object>();
            }

            var type = $"{typeof(T).Name}{typeof(TKey).Name}";

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(Repository<,>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T), typeof(TKey)), _context);
                if(repositoryInstance != null)
                    _repositories[type] = repositoryInstance;
            }
            return (IRepository<T, TKey>)_repositories[type];
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }


}