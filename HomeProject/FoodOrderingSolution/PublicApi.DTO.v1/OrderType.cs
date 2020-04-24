using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    // for display only
    public class OrderType : OrderTypeEdit
    {
        
    }

    // for display only
    public class OrderTypeDetail : OrderTypeEdit
    {
        
    }

    // from client to server
    public class OrderTypeEdit: OrderTypeCreate
    {
        public Guid Id { get; set; }
    }
    // from client to server
    public class OrderTypeCreate
    {
        [MaxLength(256)] [MinLength(1)] public string Name { get; set; } = default!;
        [MaxLength(1024)] public string? Comment { get; set; }
    }
}