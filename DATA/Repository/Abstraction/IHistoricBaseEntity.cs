namespace DATA.Repository.Implementation
{
    public interface IHistoricBaseEntity
    {
        DateTime PeriodEnd { get; set; }
        DateTime PeriodStart { get; set; }
    }

}
