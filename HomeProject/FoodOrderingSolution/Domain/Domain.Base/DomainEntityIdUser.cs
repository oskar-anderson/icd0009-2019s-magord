using System;
using Contracts.Domain.Base;
using Microsoft.AspNetCore.Identity;

namespace Domain.Base
{
    public abstract class DomainEntityIdUser<TUser> : DomainEntityIdUser<Guid, TUser>,IDomainEntityId, IDomainEntityUser<TUser>
        where TUser : IdentityUser<Guid>
    {
    }

    public abstract class DomainEntityIdUser<TKey, TUser> : DomainEntityId<TKey>,
        IDomainEntityUser<TKey, TUser>
        where TKey : IEquatable<TKey>
        where TUser: IdentityUser<TKey>
    {
        public TKey AppUserId { get; set; } = default!;

        public TUser? AppUser { get; set; }
    }
}