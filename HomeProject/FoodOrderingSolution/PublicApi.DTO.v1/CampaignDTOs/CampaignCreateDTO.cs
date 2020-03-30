using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1.CampaignDTOs
{
    public class CampaignCreateDTO
    {
        public Guid Id { get; set; }
        
        public DateTime From { get; set; } = default!;

        public DateTime To { get; set; } = default!;
        
        [MaxLength(512)] [MinLength(1)] public string Name { get; set; } = default!;

        [MaxLength(1024)]
        public string? Comment { get; set; }
    }
}