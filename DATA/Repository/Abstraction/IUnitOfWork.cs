using DATA.Repository.Abstraction.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace DATA.Repository.Abstraction
{
    public interface IUnitOfWork<TContext> where TContext : DbContext
    {
        IDbContextTransaction BeginTransaction();
        void Commit();
        Task CommitAsync();
        void Dispose();
        IHistoricRepository<T> HistoricRepository<T>() where T : class, IHistoricEntity;
        IRepository<T> Repository<T>() where T : class, IBaseEntity;
        void Rollback();
    }
}