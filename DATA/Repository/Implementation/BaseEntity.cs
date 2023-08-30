using DATA.Repository.Abstraction;
using DATA.Repository.Implementation.PrimaryKey;

namespace DATA.Repository.Implementation
{
    public  class BaseEntity<TKey> : IBaseEntity<TKey>, IPrimaryKey<TKey>
    {
        public TKey? PrimaryKey { get; set; }
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

                var keyValues = PrimaryKey;
                return PrimaryKey.ToString() ?? "null";
            }
        }

       
    }
}
