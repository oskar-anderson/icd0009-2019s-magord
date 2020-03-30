using System;

namespace PublicApi.DTO.v1.PriceDTOs
{
    public class PriceCreateDTO
    {
        public Guid Id { get; set; }
        
        public string From { get; set; } = default!;

        public string To { get; set; } = default!;
        
        public decimal Value { get; set; } = default!;
    }
}