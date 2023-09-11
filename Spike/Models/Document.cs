using DATA.Repository.Implementation;
using DATA.Repository.Implementation.Models;

namespace Spike.Models
{
    public abstract class DocumentBase : BaseEntity
    {
        public string Title { get; set; } = string.Empty; 
        public string Description { get; set; } = string.Empty;
    }

    public abstract class HistoricDocumentBase : HistoricEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class DocumentV1 : DocumentBase
    {
        public virtual IEnumerable<AuthorV1>? AuthorsV1 { get; set; }
    }

    public class DocumentHistoricV1 : HistoricDocumentBase
    {
        public virtual IEnumerable<AuthorHistoricV1>? AuthorsHistoricV1 { get; set; }
    }
}
