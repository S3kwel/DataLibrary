using DATA.Repository.Abstraction;
using DATA.Repository.Implementation.PrimaryKey;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATA.Repository.Implementation
{
    public class BaseEntity<TKey> : IBaseEntity<TKey>, IPrimaryKey<TKey>
    {
        [Key]
        public TKey? PrimaryKey { get; set; }
        public Guid VersionTag { get; set; }
        public bool IsDeleted { get; set; }
        public string StringId
        {
            get
            {
                return PrimaryKey?.ToString() ?? string.Empty;
            }
        }
    }

}
