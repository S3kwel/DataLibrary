using DATA.Repository.Implementation;
using DATA.Repository.Implementation.PrimaryKey;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

public class CompositePrimaryKey<T> : IPrimaryKey where T : BaseEntity
{
    public object[] Keys { get; set; }

    private static readonly string[] KeyPropertyNames = { "Id1", "Id2" /*... add other property names here ... */ };

    

    public object[] GetKeyValues()
    {
        return Keys;
    }

    public string[] GetKeyProperties()
    {
        return KeyPropertyNames;
    }

    public CompositePrimaryKey(params object[] keys)
    {
        Keys = keys;
    }
}

