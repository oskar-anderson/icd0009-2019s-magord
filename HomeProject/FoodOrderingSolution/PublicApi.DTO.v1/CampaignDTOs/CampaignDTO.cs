using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1.CampaignDTOs
{
    public class CampaignDTO
    {
        public Guid Id { get; set; }
        
        public string From { get; set; } = default!;

        public string To { get; set; } = default!;
        
        [MaxLength(512)] [MinLength(1)] public string Name { get; set; } = default!;

        [MaxLength(1024)]
        public string? Comment { get; set; }
    }
}