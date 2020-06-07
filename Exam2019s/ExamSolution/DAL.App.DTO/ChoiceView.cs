using System;

namespace DAL.App.DTO
{
    public class ChoiceView
    {
        public Guid Id { get; set; }
        
        public string Value { get; set; } = default!;
        
        public bool IsSelected { get; set; }

        public bool IsAnswer { get; set; }
        
        public Guid QuestionId { get; set; }
        public Question? Question { get; set; }
    }
}