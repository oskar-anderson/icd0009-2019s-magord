using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class TownCreateDTO
    {
        [MaxLength(256)] [MinLength(1)] public string Name { get; set; } = default!;
    }
}