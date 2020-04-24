using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    // for display only
    public class Area : AreaEdit
    {
        public ICollection<Town> Towns { get; set; } = default!;
    }

    // for display only
    public class AreaDetail : AreaEdit
    {
        public Town Town { get; set; } = default!;
    }

    // from client to server
    public class AreaEdit: AreaCreate
    {
        public Guid Id { get; set; }
    }
    // from client to server
    public class AreaCreate
    {
        [MaxLength(256)] [MinLength(1)] public string Name { get; set; } = default!;
        public Guid TownId { get; set; }
    }
}