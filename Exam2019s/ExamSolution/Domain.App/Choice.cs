using System;
using ee.itcollege.magord.healthyfood.Domain.Base;

namespace Domain.App
{
    public class Choice : DomainEntityIdMetadata
    {
        public string Value { get; set; } = default!;
        
        public bool IsSelected { get; set; }

        public bool IsAnswer { get; set; }

        public Guid QuestionId { get; set; }
        public Question? Question { get; set; }

    }
}