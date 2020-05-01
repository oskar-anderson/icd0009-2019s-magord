using System;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;

namespace PublicApi.DTO.v1
{
    public class Person : IDomainEntityId
    {
        public Guid Id { get; set; }
        [MaxLength(128)] [MinLength(1)] public string FirstName { get; set; } = default!;
        [MaxLength(128)] [MinLength(1)] public string LastName { get; set; } = default!;
        public char Sex { get; set; } = default!;
        public string DateOfBirth { get; set; } = default!;
        public Guid AppUserId { get; set; }
        
    }
}