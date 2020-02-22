using System;

namespace Contracts.DAL.Base
{
    public interface IDomainEntity:IDomainEntity<string>
    {
    }
    
    public interface IDomainEntity<TKey>
        where TKey : IComparable
    {
        TKey Id { get; set; }
    }
}