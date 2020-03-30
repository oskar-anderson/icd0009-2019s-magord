using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1.OrderDTOs
{
    public class OrderCreateDTO
    {
        public Guid Id { get; set; }
        
        [MaxLength(256)] [MinLength(1)] public string OrderStatus { get; set; } = default!;

        public int Number { get; set; } = default!;

        public DateTime TimeCreated { get; set; } = default!;
    }
}