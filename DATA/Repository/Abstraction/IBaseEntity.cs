namespace DATA.Repository.Abstraction
{
    public interface IBaseEntity
    {
        Guid Id { get; }
        string StringId { get; }    
        DateTime PeriodStart { get; set; }
        DateTime PeriodEnd { get; set; }
        Guid VersionTag { get; set; }
    }

}
