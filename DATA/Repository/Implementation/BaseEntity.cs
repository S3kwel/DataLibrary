using DATA.Repository.Abstraction;
using DATA.Repository.Implementation.PrimaryKey;

namespace DATA.Repository.Implementation
{
    public abstract class BaseEntity
    {
        public IPrimaryKey? PrimaryKey { get; set; } = null; 

        public BaseEntity()
        {

        }

       

        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
        public Guid VersionTag { get; set; }
        public bool IsDeleted { get; set; }
        public string StringId
        {
            get
            {
                if (PrimaryKey == null)
                    return string.Empty;

                var keyValues = PrimaryKey.GetKeyValues();
                return string.Join("-", keyValues.Select(v => v?.ToString() ?? "null"));
            }
        }


    }
}
