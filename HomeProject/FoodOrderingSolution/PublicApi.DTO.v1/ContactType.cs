using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    // for display only
    public class ContactType : ContactTypeEdit
    {
        
    }

    // for display only
    public class ContactTypeDetail : ContactTypeEdit
    {
        
    }

    // from client to server
    public class ContactTypeEdit: ContactTypeCreate
    {
        public Guid Id { get; set; }
    }
    // from client to server
    public class ContactTypeCreate
    {
        [MaxLength(256)] [MinLength(1)] public string Name { get; set; } = default!;
    }
}