namespace DATA.Repository.Abstraction
{
    public interface IDataDBContext
    {
        int SaveChanges();
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
    }
}