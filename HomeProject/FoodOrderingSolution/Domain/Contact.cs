using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class Contact : DomainEntity
    {
        [MaxLength(256)] [MinLength(1)] public string Name { get; set; } = default!;
        
        [MaxLength(36)] public string PersonId { get; set; } = default!;
        public Person? Person { get; set; }
        
        [MaxLength(36)] public string ContactTypeId { get; set; } = default!;
        public ContactType? ContactType { get; set; }


    }
}