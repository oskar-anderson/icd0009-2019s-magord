using System;

namespace ee.itcollege.magord.healthyfood.Contracts.Domain.Base
{
    public interface IDomainEntityMetadata
    {
        string? CreatedBy { get; set; }
        DateTime CreatedAt { get; set; }
        string? ChangedBy { get; set; }
        DateTime ChangedAt { get; set; }
        
        
        /*
        NO SOFT UPDATES/DELETES initially
        
        string? DeletedBy { get; set; }
        DateTime? DeletedAt { get; set; }
        */
    }
}