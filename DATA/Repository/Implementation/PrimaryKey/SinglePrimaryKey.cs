namespace DATA.Repository.Implementation.PrimaryKey
{
    public class SinglePrimaryKey<T> : IPrimaryKey where T : BaseEntity, new()  
    {
        public T? Key { get; set; }

        public string KeyPropertyName => "Id";

        // Fetches the value of the "Id" property of the Key. 
        // This assumes that the BaseEntity has a property named "Id".
        public object? KeyValue
        {
            get
            {
                var propertyInfo = typeof(T).GetProperty(KeyPropertyName);
                if (propertyInfo == null)
                    return null;

                return propertyInfo.GetValue(Key);
            }
        }

        public object?[] GetKeyValues() => new object?[] { KeyValue };

        public string[] GetKeyProperties() => new string[] { KeyPropertyName };

        public SinglePrimaryKey()
        {
            Key = new T();
        }
    }


}
