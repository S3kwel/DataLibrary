using DATA.Repository.Abstraction.Models;

namespace DATA.Repository.Implementation.Models
{
    public abstract class BaseEntity : IBaseEntity
    {
        public Guid Id { get; set; }
        public Guid VersionTag { get; set; }
        public bool IsDeleted { get; set; }
        public string StringId => Id.ToString();
    }
}
