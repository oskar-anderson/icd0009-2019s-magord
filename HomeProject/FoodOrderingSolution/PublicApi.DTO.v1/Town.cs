using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class Town : TownEdit
    {
        public int AreaCount { get; set; }
    }

    public class TownDetail : TownEdit
    {
        
    }
    
    public class TownCreate
    {
        [MaxLength(256)] [MinLength(1)] public string Name { get; set; } = default!;
    }
    
    public class TownEdit : TownCreate
    {
        public Guid Id { get; set; }
    }
}