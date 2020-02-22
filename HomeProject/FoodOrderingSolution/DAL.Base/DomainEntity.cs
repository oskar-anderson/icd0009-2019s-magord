using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Schema;
using Contracts.DAL.Base;

namespace DAL.Base
{
    public abstract class DomainEntity : IDomainEntity
    {
        [MaxLength(36)]
        public virtual string Id { get; set; } = Guid.NewGuid().ToString();
    }
}