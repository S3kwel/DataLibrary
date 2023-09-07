namespace DATA.Repository.Abstraction
{
    public interface IHistoricEntity
    {
        DateTime PeriodStart { get; set; }
        DateTime PeriodEnd { get; set; }
    }

}
