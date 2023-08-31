namespace DATA.Repository.Implementation
{
    public class HistoricBaseEntity<TKey> : BaseEntity<TKey>
    {
        public DateTime PeriodStart { get; set; } = DateTime.UtcNow;
        public DateTime PeriodEnd { get; set; } = DateTime.MaxValue;
    }

}
