using DATA.Repository.Abstraction.Models;
using DATA.Repository.Implementation.Filtering;

namespace DATA.Repository.Implementation.Specifications
{
    public class ByVersionTagSpecification<T> : Specification<T> where T : IBaseEntity
    {
        public ByVersionTagSpecification(Guid tag){
            Criteria = e => e.VersionTag == tag; 
        }
    }
}
