using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    // for display only
    public class Campaign : CampaignEdit
    {
        
    }

    // for display only
    public class CampaignDetail : CampaignEdit
    {
        
    }

    // from client to server
    public class CampaignEdit: CampaignCreate
    {
        public Guid Id { get; set; }
    }
    // from client to server
    public class CampaignCreate
    {
        public string From { get; set; } = default!;
        public string To { get; set; } = default!;
        [MaxLength(512)] [MinLength(1)] public string Name { get; set; } = default!;
        [MaxLength(1024)] public string? Comment { get; set; }
    }
}