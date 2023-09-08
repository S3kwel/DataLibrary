using DATA.Repository.Abstraction;
using DATA.Repository.Implementation.Filtering;
using DATA.Repository.Implementation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DATA.Repository.Implementation.Specifications
{
    public class ByVersionTagSpecification<T> : Specification<T> where T : BaseEntity
    {
        public ByVersionTagSpecification(Guid tag){
            Criteria = e => e.VersionTag == tag; 
        }
    }
}
