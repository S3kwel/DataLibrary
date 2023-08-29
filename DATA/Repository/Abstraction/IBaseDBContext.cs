namespace DATA.Repository.Abstraction
{
    public interface IBaseDBContext
    {
        int SaveChanges();
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
    }
}