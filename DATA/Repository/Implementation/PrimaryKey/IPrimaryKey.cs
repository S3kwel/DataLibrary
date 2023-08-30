namespace DATA.Repository.Implementation.PrimaryKey
{
    public interface IPrimaryKey
    {
        // Common properties and methods that don't need generics.
        object?[] GetKeyValues();
        string[] GetKeyProperties();
    }

    public interface IPrimaryKey<TKey>
    {
        TKey? PrimaryKey { get; set; }
    }





}
