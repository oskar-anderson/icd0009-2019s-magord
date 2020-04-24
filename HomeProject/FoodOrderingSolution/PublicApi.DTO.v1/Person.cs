using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    // for display only
    public class Person : PersonEdit
    {
        
    }

    // for display only
    public class PersonDetail : PersonEdit
    {
        
    }

    // from client to server
    public class PersonEdit: PersonCreate
    {
        public Guid Id { get; set; }
    }
    // from client to server
    public class PersonCreate
    {
        [MaxLength(128)] [MinLength(1)] public string FirstName { get; set; } = default!;
        [MaxLength(128)] [MinLength(1)] public string LastName { get; set; } = default!;
        public char Sex { get; set; } = default!;
        public string DateOfBirth { get; set; } = default!;
    }
}