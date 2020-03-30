using System;

namespace PublicApi.DTO.v1.PriceDTOs
{
    public class PriceCreateDTO
    {
        public Guid Id { get; set; }
        
        public DateTime From { get; set; } = default!;

        public DateTime To { get; set; } = default!;
        
        public decimal Value { get; set; } = default!;
    }
}