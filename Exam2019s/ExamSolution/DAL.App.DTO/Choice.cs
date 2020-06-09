using System;
using ee.itcollege.magord.healthyfood.Contracts.Domain.Base;

namespace DAL.App.DTO
{
    public class Choice : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public string Value { get; set; } = default!;
        
        public bool IsSelected { get; set; }

        public bool IsAnswer { get; set; }
        
        public int? NumberOfAnswers { get; set; }

        public Guid QuestionId { get; set; }
        public Question? Question { get; set; }

    }
}