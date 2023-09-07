using DATA.Repository.Abstraction;
using DATA.Repository.Implementation.Models;

namespace DATA.Repository.Implementation
{
    public abstract class HistoricEntity : BaseEntity, IHistoricEntity
    {
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
    }
}
