using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1.RestaurantDTOs
{
    public class RestaurantDTO
    {
        public Guid Id { get; set; }
        
        [MaxLength(256)] [MinLength(1)] public string Name { get; set; } = default!;

        [MaxLength(512)] [MinLength(1)] public string Address { get; set; } = default!;

        public DateTime OpenedFrom { get; set; } = default!;

        public DateTime ClosedFrom { get; set; } = default!;

        public Guid AreaId { get; set; } = default!;

    }
}