using System;

namespace DAL.App.DTO
{
    public class ContactView
    {
        public Guid Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string ContactType { get; set; } = default!;
    }
}