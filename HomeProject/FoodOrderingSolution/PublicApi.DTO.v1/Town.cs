using System;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain.Base;

namespace PublicApi.DTO.v1
{
    public class Town : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        [MaxLength(256)] [MinLength(1)] public string Name { get; set; } = default!;
    }
}
