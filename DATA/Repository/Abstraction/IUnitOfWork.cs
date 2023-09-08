using DATA.Repository.Implementation;
using DATA.Repository.Implementation.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace DATA.Repository.Abstraction
{
    public interface IUnitOfWork
    {
        IDbContextTransaction BeginTransaction();
        void Commit();
        Task CommitAsync();
        void Dispose();
        IHistoricRepository<T> HistoricRepository<T>() where T : HistoricEntity;
        IRepository<T> Repository<T>() where T : BaseEntity;
        void Rollback();
    }
}