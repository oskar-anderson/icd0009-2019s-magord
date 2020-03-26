using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class ContactDTO
    {
        public Guid Id { get; set; }
        
        [MaxLength(256)] [MinLength(1)] public string Name { get; set; } = default!;
        
        public Guid PersonId { get; set; } = default!;
        public Guid ContactTypeId { get; set; } = default!;
        
    }
}