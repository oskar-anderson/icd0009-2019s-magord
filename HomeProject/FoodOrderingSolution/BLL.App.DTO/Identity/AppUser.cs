using System;

namespace BLL.App.DTO.Identity
{
    public class AppUser : AppUser<Guid>
    {
        
    }
    
    public class AppUser<TKey>
        where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;

    }
}