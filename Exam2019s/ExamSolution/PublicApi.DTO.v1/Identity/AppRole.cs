using System;
using ee.itcollege.magord.healthyfood.Contracts.Domain.Base;

namespace PublicApi.DTO.v1.Identity
{
    public class AppRole : IDomainEntityId
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
    }
}