using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1.Identity
{
    public class RegisterDTO
    {
        [MaxLength(256)]
        [EmailAddress]
        public string Email { get; set; } = default!;
        
        [MinLength(6)]
        [MaxLength(100)]
        public string Password { get; set; } = default!;
        
        [MinLength(1)]
        [MaxLength(128)]
        public string FirstName { get; set; } = default!;
        
        [MinLength(1)]
        [MaxLength(128)]
        public string LastName { get; set; } = default!;
        
    }
}