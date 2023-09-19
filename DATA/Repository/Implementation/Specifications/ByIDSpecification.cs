using DATA.Repository.Abstraction.Models;
using DATA.Repository.Implementation.Filtering;

namespace DATA.Repository.Implementation.Specifications
{
    public class ByIdSpecification<T> : Specification<T> where T : IBaseEntity
    {
        public ByIdSpecification(string id){
            Criteria = e => e.StringId == id;
        }
    }
}
