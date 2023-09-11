using DATA.Repository.Abstraction;
using DATA.Repository.Implementation.Models;

namespace Spike.Models
{
    public abstract class AuthorBase : BaseEntity
    {
        public string FirstName { get; set; } = string.Empty; 
        public string LastName { get; set; } = string.Empty;
    }

    public abstract class HistoricAuthorBase : AuthorBase, IHistoricEntity
    {
        public DateTime PeriodStart { get; set; }   
        public DateTime PeriodEnd { get; set; } 
    }

    public class AuthorV1 : AuthorBase
    {
        public virtual IEnumerable<DocumentV1>? DocumentsV1 { get; set; }
    }

    public class  AuthorHistoricV1 : HistoricAuthorBase
    {
        public virtual IEnumerable<DocumentHistoricV1>? DocumentsHistoricV1 { get; set; }
    }
}
