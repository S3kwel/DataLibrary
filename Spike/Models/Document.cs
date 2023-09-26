using DATA.Repository.Abstraction;
using DATA.Repository.Abstraction.Models;

namespace Spike.Models
{
    public class DocumentV1 : IBaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public virtual IEnumerable<AuthorV1>? AuthorsV1 { get; set; }
        public Guid Id { get; set; }
        public Guid VersionTag { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class DocumentHistoricV1 : IHistoricEntity, IBaseEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
        public Guid VersionTag { get; set; }
        public bool IsDeleted { get; set; }
        IEnumerable<DocumenV1AuthorV1HJoin> DocumentAuthors { get; set; }
    }

}
