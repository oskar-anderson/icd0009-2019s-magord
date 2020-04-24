using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    // for display only
    public class PaymentType : PaymentTypeEdit
    {
        
    }

    // for display only
    public class PaymentTypeDetail : PaymentTypeEdit
    {
        
    }

    // from client to server
    public class PaymentTypeEdit: PaymentTypeCreate
    {
        public Guid Id { get; set; }
    }
    // from client to server
    public class PaymentTypeCreate
    {
        [MaxLength(128)] [MinLength(1)] public string Name { get; set; } = default!;
    }
}