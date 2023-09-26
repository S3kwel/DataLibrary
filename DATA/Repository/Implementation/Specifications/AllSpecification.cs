using DATA.Repository.Abstraction.Models;
using DATA.Repository.Implementation.Filtering;

namespace DATA.Repository.Implementation.Specifications
{
    public class AllSpecification<T> : Specification<T> where T : IBaseEntity
    {
        public AllSpecification(){
            Criteria = e => true; 
        }
    }
}
