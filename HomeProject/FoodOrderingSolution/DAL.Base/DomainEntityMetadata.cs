using System;
using Contracts.DAL.Base;

namespace DAL.Base
{
    public abstract class DomainEntityMetadata : DomainEntity, IDomainEntityMetadata
    {
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}