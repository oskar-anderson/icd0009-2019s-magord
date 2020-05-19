using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;

namespace PublicApi.DTO.v1
{
    public class Order : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        [MaxLength(256)] [MinLength(1)] public string OrderStatus { get; set; } = default!;
        public int Number { get; set; } = default!;
        public string TimeCreated { get; set; } = default!;
        public Guid RestaurantId { get; set; }
        public Guid OrderTypeId { get; set; }
        public Guid PersonId { get; set; }
        public Guid AppUserId { get; set; }
        
    }
}