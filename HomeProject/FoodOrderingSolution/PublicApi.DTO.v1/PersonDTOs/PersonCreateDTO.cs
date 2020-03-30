using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class PersonCreateDTO
    {
        public Guid Id { get; set; }
        [MaxLength(128)] [MinLength(1)] public string FirstName { get; set; } = default!;
        [MaxLength(128)] [MinLength(1)] public string LastName { get; set; } = default!;
        public char Sex { get; set; } = default!;
        public DateTime DateOfBirth { get; set; } = default!;
    }
}