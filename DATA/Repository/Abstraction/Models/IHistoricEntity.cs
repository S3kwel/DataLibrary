using DATA.Repository.Abstraction.Models;

namespace DATA.Repository.Abstraction
{
    public interface IHistoricEntity : IBaseEntity
    {
        DateTime PeriodStart { get; set; }
        DateTime PeriodEnd { get; set; }
    }

}
