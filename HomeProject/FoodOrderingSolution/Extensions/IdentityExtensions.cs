using System;
using System.Linq;
using System.Security.Claims;

namespace Extensions
{
    public static class IdentityExtensions
    {
        public static string UserId(this ClaimsPrincipal user)
        {
            return user.Claims.Single(c =>
                c.Type == ClaimTypes.NameIdentifier).Value;
        }
    }
}