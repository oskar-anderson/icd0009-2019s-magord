using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    // for display only
    public class Contact : ContactEdit
    {
        public ICollection<Person> Persons { get; set; } = default!;
        public ICollection<ContactType> ContactTypes { get; set; } = default!;
    }

    // for display only
    public class ContactDetail : ContactEdit
    {
        public Person Person { get; set; } = default!;
        public ContactType ContactType { get; set; } = default!;
    }

    // from client to server
    public class ContactEdit: ContactCreate
    {
        public Guid Id { get; set; }
    }
    // from client to server
    public class ContactCreate
    {
        [MaxLength(256)] [MinLength(1)] public string Name { get; set; } = default!;
        public Guid PersonId { get; set; } = default!;
        public Guid ContactTypeId { get; set; } = default!;
    }
}