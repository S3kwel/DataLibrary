namespace DATA.Repository.Abstraction
{
    public class OrderingCriteria
    {
        public string PropertyName { get; private set; } = string.Empty;
        public bool Descending { get; private set; } = false;

        // Fluent API methods

        public OrderingCriteria WithPropertyName(string propertyName)
        {
            PropertyName = propertyName;
            return this;
        }

        public OrderingCriteria SetDescending(bool descending = true)
        {
            Descending = descending;
            return this;
        }
    }



}