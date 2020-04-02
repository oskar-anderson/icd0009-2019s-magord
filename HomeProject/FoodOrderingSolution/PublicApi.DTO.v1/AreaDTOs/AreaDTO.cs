using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1.AreaDTOs
{
    public class AreaDTO
    {
        public Guid Id { get; set; }
        [MaxLength(256)] [MinLength(1)] public string Name { get; set; } = default!;
        public Guid TownId { get; set; } = default!;
        public TownDTO Town { get; set; } = default!;
        public int RestaurantCount { get; set; }
    }
}