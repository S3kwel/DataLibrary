using DATA.Repository.Abstraction;
using DATA.Repository.Abstraction.Models;
using System.ComponentModel.DataAnnotations;

namespace Spike.Models
{
    public class AuthorV1: IBaseEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public virtual IEnumerable<DocumentV1>? DocumentsV1 { get; set; }
        public Guid VersionTag { get; set; }
        public bool IsDeleted { get; set; }
        public string StringId { get; set; }
    }

    public class AuthorHistoricV1 : IHistoricEntity, IBaseEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
        IEnumerable<DocumenV1AuthorV1HJoin> DocumentAuthors { get; set; }    
        public Guid VersionTag { get; set; }
        public bool IsDeleted { get; set; }
        public string StringId { get; set; } = string.Empty; 
    }

    //Make this historic.
    //Make these entities use a common interface.
    //Should only be needed for historic versions of entities.  
    //If the user doesn't want navigations, they can just disinclude them. 
    public class DocumenV1AuthorV1HJoin : IJoinEntity<DocumentHistoricV1, AuthorHistoricV1>
    {
        //Custom property names
        [Key]
        public Guid JoinId { get; set; }    
        public Guid DocmentV1ID { get; set; }
        public DocumentHistoricV1 DocumentHistoricV1 { get; set; }
        public Guid AuthorV1ID { get; set; }
        public AuthorHistoricV1 AuthorHistoricV1 { get; set; }

        public Guid JoinID { get {  return JoinId; } set { JoinId = value; } }
        public Guid Type1ID { get { return DocmentV1ID; } set { DocmentV1ID = value; } }
        public DocumentHistoricV1 Type1 { get { return DocumentHistoricV1; } set { DocumentHistoricV1 = value; } }
        public Guid Type2ID { get { return DocmentV1ID; } set { DocmentV1ID = value; } }
        public AuthorHistoricV1 Type2 { get { return AuthorHistoricV1; } set { AuthorHistoricV1 = value; } }


    }
}
