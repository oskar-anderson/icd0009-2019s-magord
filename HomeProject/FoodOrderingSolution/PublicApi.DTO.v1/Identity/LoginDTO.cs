﻿namespace PublicApi.DTO.v1.Identity
{
    public class LoginDTO
    {
        public string Email { get; set; } = default!;
        
        public string Password { get; set; } = default!;
    }
}