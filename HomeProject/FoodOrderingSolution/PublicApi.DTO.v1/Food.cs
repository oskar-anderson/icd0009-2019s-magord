using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;

namespace PublicApi.DTO.v1
{
    public class Food : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        [MaxLength(1024)] public string? Description { get; set; }
        [MaxLength(256)] [MinLength(1)] public string Name { get; set; } = default!;
        public int Amount { get; set; } = default!;
        public float Size { get; set; } = default!;
        public Guid FoodTypeId { get; set; } = default!;
    }
}