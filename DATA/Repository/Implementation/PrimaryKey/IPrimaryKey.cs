namespace DATA.Repository.Implementation.PrimaryKey
{
    public interface IPrimaryKey
    {
        // Common properties and methods that don't need generics.
        object?[] GetKeyValues();
        string[] GetKeyProperties();
    }

    public interface IPrimaryKey<T> : IPrimaryKey where T : BaseEntity
    {
        T? Key { get; set; }
    }




}
