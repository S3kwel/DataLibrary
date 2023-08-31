﻿using DATA.Repository.Implementation.PrimaryKey;

namespace DATA.Repository.Abstraction
{
    public interface IBaseEntity<TKey>
    {
        bool IsDeleted { get; set; }
        TKey? PrimaryKey { get; set; }
        string StringId { get; }
        Guid VersionTag { get; set; }
    }
}