namespace DATA.Repository.Abstraction.Models
{
    public interface IBaseEntity
    {
        Guid Id { get; }
        Guid VersionTag { get; set; }
        public bool IsDeleted { get; set; }
        string StringId { get; }
    }

}
